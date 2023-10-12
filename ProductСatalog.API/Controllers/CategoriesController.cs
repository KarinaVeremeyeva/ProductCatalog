﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Models;
using ProductCatalog.BLL.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            var categoryModel = _mapper.Map<CategoryModel>(categoryDto);
            var addedCategory = await _categoryService.CreateCategoryAsync(categoryModel);
            var result = _mapper.Map<CategoryDto>(addedCategory);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            var categoryModel = _mapper.Map<CategoryModel>(categoryDto);
            var addedCategory = await _categoryService.UpdateCategoryAsync(categoryModel);
            var result = _mapper.Map<CategoryDto>(addedCategory);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            await _categoryService.RemoveCategoryAsync(id);

            return Ok();
        }
    }
}
