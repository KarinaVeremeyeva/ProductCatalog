using AutoMapper;
using ProductCatalog.BLL.Models;
using ProductCatalog.DAL.Entities;
using ProductCatalog.DAL.Repositories;

namespace ProductCatalog.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
        {
            var categoryToAdd = _mapper.Map<Category>(category);
            var addedCategory = await _categoryRepository.CreateAsync(categoryToAdd);
            var categoryModel = _mapper.Map<CategoryModel>(addedCategory);

            return categoryModel;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);

            return categoryModels;
        }

        public async Task<CategoryModel?> GetCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            var categoryModel = _mapper.Map<CategoryModel>(category);

            return categoryModel;
        }

        public async Task RemoveCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return;
            }

            await _categoryRepository.RemoveAsync(category.Id);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(Guid id, CategoryModel category)
        {
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException($"Category {id} was not found");
            }

            categoryToUpdate.Name = category.Name;

            var updatedCategory = await _categoryRepository.UpdateAsync(categoryToUpdate);
            var categoryModel = _mapper.Map<CategoryModel>(updatedCategory);

            return categoryModel;
        }
    }
}
