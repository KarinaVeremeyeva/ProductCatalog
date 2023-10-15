using ProductCatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface ICategoryService
    {
        Task<CategoryModel> CreateCategoryAsync(CategoryModel category);

        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();

        Task<CategoryModel?> GetCategoryByIdAsync(Guid id);

        Task<CategoryModel> UpdateCategoryAsync(Guid id, CategoryModel category);

        Task RemoveCategoryAsync(Guid id);
    }
}
