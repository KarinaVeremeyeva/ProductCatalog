using ProductCatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface ICategoryService
    {
        Task<CategoryModel> CreateCategoryAsync(CategoryModel category);

        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();

        Task<CategoryModel?> GetCategoryAsync(Guid categoryId);

        Task<CategoryModel> UpdateCategoryAsync(CategoryModel category);

        Task RemoveCategoryAsync(Guid categoryId);
    }
}
