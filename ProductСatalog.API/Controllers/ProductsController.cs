﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Models;
using ProductCatalog.BLL.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDto filterProductsDto)
        {
            var filterProducts = _mapper.Map<FilterProductsModel>(filterProductsDto);
            var products = await _productService.GetProductsAsync(filterProducts);
            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productsDto);
        }

        [Authorize(Roles = "AdvancedUser, User")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(UpdateProductDto productDto)
        {
            var category = await _categoryService.GetCategoryByIdAsync(productDto.CategoryId);
            if (category == null)
            {
                return BadRequest();
            }

            var productModel = _mapper.Map<ProductModel>(productDto);
            var addedProduct = await _productService.CreateProductAsync(productModel);
            var result = _mapper.Map<ProductDto>(addedProduct);

            return Ok(result);

        }

        [Authorize(Roles = "AdvancedUser, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(productDto.CategoryId);
            if (category == null)
            {
                return BadRequest();
            }

            var productModel = _mapper.Map<ProductModel>(productDto);
            var updatedProduct = await _productService.UpdateProductAsync(id, productModel);
            var result = _mapper.Map<ProductDto>(updatedProduct);

            return Ok(result);
        }

        [Authorize(Roles = "AdvancedUser")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            await _productService.RemoveProductAsync(id);

            return Ok();
        }
    }
}
