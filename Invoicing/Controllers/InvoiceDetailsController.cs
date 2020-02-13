using System.Collections.Generic;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoicing.Web.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceDetailsController : Controller
    {
        private readonly IInvoiceDetailsService _detailsService;

        public InvoiceDetailsController(IInvoiceDetailsService detailsService)
        {
            _detailsService = detailsService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]InvoiceDetails model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _detailsService.Add(model);

            return new JsonResult(model);
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
