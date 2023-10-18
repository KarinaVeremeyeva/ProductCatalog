using Microsoft.AspNetCore.Identity;
using ProductСatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface IUserService
    {
        Task<IdentityResult> ChangeUserPasswordAsync(string userId, string newPassword);

        Task<IdentityResult> CreateUserAsync(UserModel userModel);

        Task<IdentityResult> DeleteUserAsync(string id);

        Task<IEnumerable<UserModel>> GetUsersAsync();

        Task LockUserAsync(string userId, bool isLocked);

        Task<SignInResult> LoginAsync(string email, string password);

        Task LogoutAsync();

        Task<UserModel?> GetUserByEmailAsync(string email);

        Task<UserModel?> GetUserByIdAsync(string id);
    }
}
