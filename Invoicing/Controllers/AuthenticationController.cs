using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Invoicing.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/<controller>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Post([FromBody]AuthenticationModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is invalid" });
            }

            //return basic user info and authentication token
            return Ok(new
            {
                Id = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = user.Token
            });
        }
    }
}
