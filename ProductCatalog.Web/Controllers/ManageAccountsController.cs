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

        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            var response = await _userApiService.CreateUserAsync(email, password);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }

            return View();
        }

        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LockUser(string id, bool isLocked)
        {
            var response = await _userApiService.LockUserAsync(id, isLocked);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _userApiService.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeUserPassword(string id)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(string id, string newPassword)
        {
            var response = await _userApiService.ChangeUserPasswordAsync(id, newPassword);

            return RedirectToAction(nameof(Index));
        }
    }
}
