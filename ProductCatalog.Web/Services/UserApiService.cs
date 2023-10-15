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

        public async Task<IEnumerable<UserDto>?> GetUersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>($"{UsersApiPath}");
        }

        public async Task<HttpResponseMessage> CreateUserAsync(string email, string password)
        {
            return await _httpClient.PostAsJsonAsync($"{UsersApiPath}", new { email, password });
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(string id)
        {
            return await _httpClient.DeleteAsync($"{UsersApiPath}/{id}");
        }

        public async Task<HttpResponseMessage> ChangeUserPasswordAsync(string id, string newPassword)
        {
            return await _httpClient.PutAsJsonAsync($"{UsersApiPath}/{id}/password", newPassword);
        }
    }
}
