using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public class ProductApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;

        private const string ProductsPath = "api/Products";

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> CreateProductAsync(UpdateProductDto productDto)
        {
            var result = await _httpClient.PostAsJsonAsync($"{ProductsPath}", productDto);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<HttpResponseMessage> DeleteProductAsync(Guid id)
        {
            return await _httpClient.DeleteAsync($"{ProductsPath}/{id}");
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>(ProductsPath);
        }

        public async Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var result = await _httpClient.PutAsJsonAsync($"{ProductsPath}/{id}", productDto);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<ProductDto>();
        }
    }
}
