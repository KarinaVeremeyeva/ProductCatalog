using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        private const string CategoriesPath = "api/Categories";

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>?> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>(CategoriesPath);
        }

        public async Task<CategoryDto?> CreateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var result = await _httpClient.PostAsJsonAsync($"{CategoriesPath}", categoryDto);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<CategoryDto>();
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto)
        {
            var result = await _httpClient.PutAsJsonAsync($"{CategoriesPath}/{id}", categoryDto);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<CategoryDto>();
        }

        public async Task<HttpResponseMessage> DeleteCategoryAsync(Guid id)
        {
            return await _httpClient.DeleteAsync($"{CategoriesPath}/{id}");
        }
    }
}
