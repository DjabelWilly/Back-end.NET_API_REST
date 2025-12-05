using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {             
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginService model)
        {
            //TODO: implement the UserManager from Identity to validate User and return a security token.
            return Unauthorized();
        }            
    }
}