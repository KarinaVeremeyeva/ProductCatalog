using ProductCatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(ProductModel product);

        Task<IEnumerable<ProductModel>> GetProductsAsync();

        Task<ProductModel?> GetProductAsync(Guid productId);

        Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product);

        Task RemoveProductAsync(Guid productId);
    }
}
