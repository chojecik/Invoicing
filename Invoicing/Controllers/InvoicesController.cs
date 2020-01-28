using System.Collections.Generic;
using AutoMapper;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Invoicing.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoicing.Web.Controllers
{
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;
        private readonly IFileService _fileService;

        public InvoicesController(IMapper mapper, IInvoiceService invoiceService, IFileService fileService)
        {
            _mapper = mapper;
            _invoiceService = invoiceService;
            _fileService = fileService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_invoiceService.GetAll());    //TODO wyswietlac tylko faktury obecnego uzytkownika, zwraca ViewModel
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post([FromBody]InvoiceModel model)
        {
            model.FileExtension = _fileService.GetFileExtension(model.FilePath);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = _mapper.Map<Invoice>(model);
            _invoiceService.Add(invoice);
            //TODO usunąć plik z TempUpload i zapisać w docelowym katalogu
            return Ok(invoice);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
