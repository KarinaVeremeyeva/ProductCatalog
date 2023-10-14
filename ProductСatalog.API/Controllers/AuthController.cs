using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        private const string Authorization = "Authorization";

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
            if (result.IsLockedOut)
            {
                const string message = "Your account is locked out.";
                
                return BadRequest(message);
            }

            var user = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest();
            };

            var token = _tokenService.CreateToken(user.Email, user.Roles.First());
            Response.Headers.Add(Authorization, $"Bearer {token}");

            return Ok();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return Ok();
        }
    }
}
