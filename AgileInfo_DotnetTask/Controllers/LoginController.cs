using AgileInfoTask.Model;
using AgileInfoTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AgileInfoTask.Controllers
{/// <summary>
/// Task 7 we used to here to show methods,reponse,parameters etc.
/// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService; 

        public LoginController(IConfiguration configuration, AuthService authService)
        {
            _configuration = configuration;
            _authService = authService; 
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            IActionResult response = Unauthorized();
            var userFromDb = _authService.AuthenticateUser(userModel);
            if (userFromDb != null)
            {
                var token = _authService.GenerateToken(userFromDb);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}




