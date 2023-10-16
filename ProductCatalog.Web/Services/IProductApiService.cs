using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public interface IProductApiService
    {
        Task<IEnumerable<ProductDto>?> GetProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(Guid id);

        Task<ProductDto?> CreateProductAsync(UpdateProductDto productDto);

        Task<ProductDto?> UpdateProductAsync(Guid id, UpdateProductDto productDto);

        Task<HttpResponseMessage> DeleteProductAsync(Guid id);
    }
}
