using System.Collections.Generic;
using AutoMapper;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Invoicing.Core.Enums;
using Invoicing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public InvoicesController(IMapper mapper, IInvoiceService invoiceService, IFileService fileService, IUserService userService)
        {
            _mapper = mapper;
            _invoiceService = invoiceService;
            _fileService = fileService;
            _userService = userService;
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
            model.FileExtension = _fileService.GetFileExtension(model.FilePath);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = _mapper.Map<Invoice>(model);
            invoice.Attachment.DirectoryPath = _fileService.MoveFile(invoice.Attachment.DirectoryPath, invoice.Type, invoice.Date);
            var user = _userService.GetById(userId);
            user.Invoices.Add(invoice);
            _userService.Update(user);
            return Ok(invoice);
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
            _invoiceService.Delete(id);
            return Ok();
        }
    }
}
