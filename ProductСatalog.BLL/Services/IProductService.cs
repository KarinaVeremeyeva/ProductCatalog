using ProductCatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(ProductModel product);

        Task<IEnumerable<ProductModel>> GetProductsAsync(FilterProductsModel filterProduct);

        Task<ProductModel?> GetProductByIdAsync(Guid id);

        Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product);

        Task RemoveProductAsync(Guid id);
    }
}
