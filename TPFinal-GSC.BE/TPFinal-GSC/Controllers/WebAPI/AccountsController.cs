using Microsoft.AspNetCore.Mvc;
using System.Data;
using TPFinal_GSC.Dto;
using TPFinal_GSC.Handlers;

namespace TPFinal_GSC.Controllers.WebAPI
{
    [Route("api/")]
    public class AccountsController : ControllerBase
    {
        private readonly IJwtHandler jwtHandler;
        public AccountsController(IJwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto user)
        {
            var roles = new List<string> { };
            if (user.UserName == "admin" && user.Password == "admin")
                roles = new List<string> { "Admin" };
            else if (user.UserName == "user" && user.Password == "user")
                roles = new List<string> { "User" };
            else
                return Unauthorized("Invalid username or password");

            var bearer = jwtHandler.GenerateToken(user, roles);
            return Ok(new
            {
                token = bearer,
            });
        }
    }
}
