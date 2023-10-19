using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public interface IUserApiService
    {
        Task<IEnumerable<UserDto>?> GetUsersAsync();

        Task<UserDto?> GetUserByIdAsync(string id);

        Task<HttpResponseMessage> CreateUserAsync(CreateUserDto userDto);

        Task<HttpResponseMessage> DeleteUserAsync(string id);

        Task<HttpResponseMessage> ChangeUserPasswordAsync(string id, string newPassword);

        Task<HttpResponseMessage> LoginAsync(LoginDto loginDto);

        Task<HttpResponseMessage> LogoutAsync();

        Task<HttpResponseMessage> LockUserAsync(string id, bool isLocked);
    }
}
