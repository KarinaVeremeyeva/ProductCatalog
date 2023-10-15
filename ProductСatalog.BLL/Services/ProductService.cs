using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public ProductService(
            IProductRepository productRepository,
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                _logger.LogError($"Category {product.CategoryId} doesn't exist. Create failure");

                throw new ArgumentException($"Category {product.CategoryId} was not found");
            }

            var productToAdd = _mapper.Map<Product>(product);
            var addedProduct = await _productRepository.CreateAsync(productToAdd);
            var productModel = _mapper.Map<ProductModel>(addedProduct);

            _logger.LogInformation($"Product {product.Id} was created");

            return productModel;
        }

        public async Task<ProductModel?> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            return _mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(FilterProductsModel filterProduct)
        {
            var query = _productRepository.GetQuery();

            if (!string.IsNullOrEmpty(filterProduct.NamePart))
            {
                query = query.Where(q => q.Name.Contains(filterProduct.NamePart));
            }

            if (filterProduct.MinPrice.HasValue)
            {
                query = query.Where(q => q.Price >= filterProduct.MinPrice.Value);
            }

            if (filterProduct.MaxPrice.HasValue)
            {
                query = query.Where(q => q.Price <= filterProduct.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(filterProduct.CategoryNamePart))
            {
                query = query.Where(q => q.Category.Name.Contains(filterProduct.CategoryNamePart));
            }

            var products = await query.ToListAsync();
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
                _logger.LogError($"Product {id} doesn't exist. Update failure");

                throw new ArgumentException($"Product {id} was not found");
            }

            var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                _logger.LogError($"Category {product.CategoryId} doesn't exist. Update failure");

                throw new ArgumentException($"Category {product.CategoryId} was not found");
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Description = product.Description;
            productToUpdate.GeneralNote = product.GeneralNote;
            productToUpdate.SpecialNote = product.SpecialNote;
            productToUpdate.CategoryId = product.CategoryId;

            var updatedProduct = await _productRepository.UpdateAsync(productToUpdate);
            var productModel = _mapper.Map<ProductModel>(updatedProduct);

            _logger.LogInformation($"Product {id} was updated");

            return productModel;
        }
    }
}
