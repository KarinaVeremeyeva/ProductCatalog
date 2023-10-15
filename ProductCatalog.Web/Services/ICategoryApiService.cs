using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public interface ICategoryApiService
    {
        Task<IEnumerable<CategoryDto>?> GetCategoriesAsync();

        Task<CategoryDto?> CreateCategoryAsync(UpdateCategoryDto categoryDto);

        Task<CategoryDto?> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto);

        Task<HttpResponseMessage> DeleteCategoryAsync(Guid id);
    }
}
