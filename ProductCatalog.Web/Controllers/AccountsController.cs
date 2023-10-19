using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ProductCatalog.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IMapper _mapper;

        private const string Authorization = "Authorization";

        public AccountsController(
            IUserApiService userApiService,
            IMapper mapper)
        {
            _userApiService = userApiService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
        {
            var loginDto = _mapper.Map<LoginDto>(loginModel);

            if (ModelState.IsValid)
            {
                var response = await _userApiService.LoginAsync(loginDto);

                if (response.IsSuccessStatusCode && response.Headers.Contains(Authorization))
                {
                    var token = response.Headers.GetValues(Authorization).ToArray()[0];

                    AuthorizeHandle(token);

                    return RedirectToAction("Index", "Products");
                }
            }

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var response = await _userApiService.LogoutAsync();

            if (response.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Products");
            }

            return Forbid();
        }

        private async void AuthorizeHandle(string token)
        {
            HttpContext.Response.Cookies.Append(Authorization, token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token.Replace("Bearer ", ""));

            var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "UserInfo",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var role = jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            HttpContext.User = new GenericPrincipal(claimsIdentity, new string[] { role });

            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, HttpContext.User);
        }
    }
}
