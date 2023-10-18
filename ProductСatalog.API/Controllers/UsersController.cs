using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Models;
using ProductCatalog.BLL.Services;
using ProductСatalog.BLL.Models;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            var user = await _userService.GetUserByEmailAsync(userDto.Email);
            if (user != null)
            {
                return BadRequest();
            }

            var userModel = _mapper.Map<UserModel>(userDto);

            var result = await _userService.CreateUserAsync(userModel);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }

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

            await _userService.ChangeUserPasswordAsync(id, newPassword);

            return Ok();
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
