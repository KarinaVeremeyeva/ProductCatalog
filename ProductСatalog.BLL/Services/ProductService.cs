using AutoMapper;
using ProductCatalog.BLL.Models;
using ProductCatalog.DAL.Entities;
using ProductCatalog.DAL.Repositories;

namespace ProductCatalog.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            var productToAdd = _mapper.Map<Product>(product);
            var addedProduct = await _productRepository.CreateAsync(productToAdd);
            var productModel = _mapper.Map<ProductModel>(addedProduct);

            return productModel;
        }

        public async Task<ProductModel?> GetProductAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            return _mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            return productModels;
        }

        public async Task RemoveProductAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return;
            }

            await _productRepository.RemoveAsync(product.Id);
        }

        public async Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(id);
            if (productToUpdate == null)
            {
                throw new ArgumentException($"Product {id} was not found");
            }

            var category = await _categoryService.GetCategoryAsync(productToUpdate.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category {productToUpdate.CategoryId} was not found");
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Description = product.Description;
            productToUpdate.GeneralNote = product.GeneralNote;
            productToUpdate.SpecialNote = product.SpecialNote;
            productToUpdate.CategoryId = product.CategoryId;

            var updatedProduct = await _productRepository.UpdateAsync(productToUpdate);
            var productModel = _mapper.Map<ProductModel>(updatedProduct);

            return productModel;
        }
    }
}
