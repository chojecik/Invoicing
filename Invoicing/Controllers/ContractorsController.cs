﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoicing.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ContractorsController : Controller
    {
        private readonly IUserService _userService;

        public ContractorsController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<controller>
        [HttpGet("user-contractors")]
        public IActionResult GetUserContractors()
        {
            int.TryParse(HttpContext.User.Identity.Name, out int userId);
            return new JsonResult(_userService.GetUserContractors(userId));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Contractor contractor)
        {
            if(contractor == null)
            {
                return BadRequest();
            }

            int.TryParse(HttpContext.User.Identity.Name, out int userId);
            var user = _userService.GetById(userId);
            user.Contractors.Add(contractor);
            _userService.Update(user);
            return Ok(contractor);
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
