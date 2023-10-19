using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;

        private const string AuthApiPath = "api/Auth";
        private const string UsersApiPath = "api/Users";

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDto loginDto)
        {
            return await _httpClient.PostAsJsonAsync($"{AuthApiPath}/login", loginDto);
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            return await _httpClient.GetAsync($"{AuthApiPath}/logout");
        }

        public async Task<IEnumerable<UserDto>?> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>($"{UsersApiPath}");
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"{UsersApiPath}/{id}");
        }

        public async Task<HttpResponseMessage> CreateUserAsync(CreateUserDto userDto)
        {
            return await _httpClient.PostAsJsonAsync($"{UsersApiPath}", userDto);
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(string id)
        {
            return await _httpClient.DeleteAsync($"{UsersApiPath}/{id}");
        }

        public async Task<HttpResponseMessage> ChangeUserPasswordAsync(string id, string newPassword)
        {
            return await _httpClient.PutAsJsonAsync($"{UsersApiPath}/{id}/password", newPassword);
        }

        public async Task<HttpResponseMessage> LockUserAsync(string id, bool isLocked)
        {
            return await _httpClient.PutAsync($"{UsersApiPath}/{id}/lock/{isLocked}", null);
        }
    }
}
