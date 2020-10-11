using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.API.Models;
using Server.API.Repositories;
using Server.API.Services;
using System.Threading.Tasks;

namespace Server.API.Controllers
{
    [Route("v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _token;

        public AccountController(IOptions<AppSettings> appSettings)
        {
            _token = new TokenService(appSettings);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLoginCommand userLogin)
        {
            var user = UserRepository.GetUser(userLogin.Email, userLogin.Password);

            if (user == null)
                return NotFound(new { message = "User or password is invalid" });

            var userLoginViewModel = new UserLoginViewModel()
            {
                Email = user.Email,
                Token = _token.GenerateToken(user)
            };

            return Ok(userLoginViewModel);
        }
    }
}