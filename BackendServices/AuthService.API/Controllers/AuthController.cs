using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public AuthController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _userAppService.LoginUser(loginDTO);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("Invalid email or password.");
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpDTO signUpDTO, string Role)
        {
            bool isRegistered = _userAppService.SignUpUser(signUpDTO, Role);
            if (isRegistered)
            {
                return Ok("User registered successfully.");
            }
            return BadRequest("User registration failed.");
        }
    }
}
