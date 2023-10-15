using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;

namespace ProductCatalog.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IMapper _mapper;

        public AccountsController(IUserApiService userApiService, IMapper mapper)
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
            var response = await _userApiService.LoginAsync(loginDto);

            if (ModelState.IsValid)
            {
                if (response.IsSuccessStatusCode && response.Headers.Contains("Authorization"))
                {
                    return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }

            return Forbid();
        }
    }
}
