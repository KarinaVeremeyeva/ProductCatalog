using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public interface IUserApiService
    {
        Task<IEnumerable<UserDto>?> GetUsersAsync();

        Task<HttpResponseMessage> CreateUserAsync(string email, string password);

        Task<HttpResponseMessage> DeleteUserAsync(string id);

        Task<HttpResponseMessage> ChangeUserPasswordAsync(string id, string newPassword);

        Task<HttpResponseMessage> LoginAsync(LoginDto loginDto);

        Task<HttpResponseMessage> LogoutAsync();
    }
}
