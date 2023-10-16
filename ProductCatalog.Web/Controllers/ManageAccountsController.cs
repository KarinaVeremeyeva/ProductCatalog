using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;

namespace ProductCatalog.Web.Controllers
{
    public class ManageAccountsController : Controller
    {
        private readonly IUserApiService _userApiService;
        private readonly IMapper _mapper;

        public ManageAccountsController(
            IUserApiService userApiService,
            IMapper mapper)
        {
            _userApiService = userApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userApiService.GetUsersAsync();
            var usersViewModels = _mapper.Map<List<UserViewModel>>(users);
            
            return View(usersViewModels);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
