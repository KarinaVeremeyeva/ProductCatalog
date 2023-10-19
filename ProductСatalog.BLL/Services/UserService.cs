using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductСatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(UserModel userModel)
        {
            var user = new IdentityUser
            {
                Email = userModel.Email,
                NormalizedEmail = userModel.Email.ToUpper(),
                UserName = userModel.Email,
                NormalizedUserName = userModel.Email.ToUpper()
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserModel>();

            foreach (var user in users)
            {
                var userWithRoles = _mapper.Map<UserModel>(user);
                var roles = await _userManager.GetRolesAsync(user);

                userWithRoles.Roles = roles;
                userWithRoles.IsLocked = user.LockoutEnd != null;
                usersWithRoles.Add(userWithRoles);
            }

            return usersWithRoles;
        }

        public async Task LockUserAsync(string userId, bool isLocked)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            user.LockoutEnabled = isLocked;
            user.LockoutEnd = isLocked ? DateTime.Now.AddYears(100) : null;

            await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: false);
            
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userModel = _mapper.Map<UserModel>(user);
            userModel.Roles = roles.ToList();

            return userModel;
        }

        public async Task<UserModel?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }
    }
}
