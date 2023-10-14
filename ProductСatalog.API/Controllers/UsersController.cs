using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users =  await _userService.GetUsersAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return BadRequest();
            }

            await _userService.CreateUserAsync(email, password);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);

            return Ok();
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangeUserPassword(string id, [FromBody] string newPassword)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.ChangeUserPasswordAsync(id, newPassword);

            return Ok(result);
        }

        [HttpPut("{id}/lock/{isLocked}")]
        public async Task<IActionResult> LockUser(string id, bool isLocked)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.LockUserAsync(id, isLocked);

            return Ok();
        }
    }
}
