using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using DinkToPdf.Contracts;
using Invoicing.BusinessLogic.Factories;
using Invoicing.BusinessLogic.Factories.Templates;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Invoicing.Core.Enums;
using Invoicing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoicing.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IConverter _converter;
        private readonly PdfFactory _pdfFactory;
        public InvoicesController(IMapper mapper, IInvoiceService invoiceService, IFileService fileService, IUserService userService, IConverter converter)
        {
            _mapper = mapper;
            _invoiceService = invoiceService;
            _fileService = fileService;
            _userService = userService;
            _converter = converter;
            _pdfFactory = new PdfFactory(_converter);
        }

        // GET: api/<controller>
        [HttpGet("invoices/{type}")]
        public IActionResult GetCostInvoices(int type)
        {
            int.TryParse(HttpContext.User.Identity.Name, out int userId);
            return new JsonResult(_mapper
                .Map<List<InvoiceModel>>(_userService.GerUsersInvoicesByType(userId, (InvoiceType)type)));    
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Id must be positive value");
            }

            var invoice = _invoiceService.GetById(id);

            var invoiceModel = _mapper.Map<InvoiceModel>(invoice);
            return new JsonResult(invoiceModel);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]InvoiceModel model)
        {
            int.TryParse(HttpContext.User.Identity.Name, out int userId);

            if(!string.IsNullOrEmpty(model.FilePath))
            {
                model.FileExtension = _fileService.GetFileExtension(model.FilePath);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = _mapper.Map<Invoice>(model);
            if(!string.IsNullOrEmpty(invoice.Attachment.DirectoryPath))
            {
                invoice.Attachment.DirectoryPath = _fileService.MoveFile(invoice.Attachment.DirectoryPath, invoice.Type, invoice.DateOfIssue);
            }
            var user = _userService.GetById(userId);
            user.Invoices.Add(invoice);
            _userService.Update(user);
            return Ok(invoice);
        }

        [HttpPost("generate-new")]
        public IActionResult GenerateNewInvoice([FromBody]InvoiceModel model)
        {
            int.TryParse(HttpContext.User.Identity.Name, out int userId);
            var user = _userService.GetById(userId);

            var invoice = _mapper.Map<Invoice>(model);
            invoice.DateOfPayment = DateTime.Now.AddDays(15);    

            var file = _pdfFactory.GeneratePdf(invoice, TemplateGenerator.GetSaleInvoiceTemplate(invoice, user));

            return File(file, "application/pdf", $"{invoice.Number.Replace("'/'", "_")}.pdf");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Id must be positive value");
            }
            var invoice = _invoiceService.GetById(id);
            _fileService.DeleteFile(invoice.Attachment.DirectoryPath);
            _invoiceService.Delete(invoice);
            return Ok();
        }
    }
}
