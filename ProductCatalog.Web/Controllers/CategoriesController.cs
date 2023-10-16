using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;

namespace ProductCatalog.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryApiService categoryApiService,
            IMapper mapper)
        {
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            var categoriesViewModels = _mapper.Map<List<CategoryViewModel>>(categories);
            
            return View(categoriesViewModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<UpdateCategoryDto>(categoryViewModel);
                var response = await _categoryApiService.CreateCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }

            return View(categoryViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryToUpdate = await _categoryApiService.GetCategoryByIdAsync(id);
            if (categoryToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var category = _mapper.Map<CategoryViewModel>(categoryToUpdate);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<UpdateCategoryDto>(categoryViewModel);
                var response = await _categoryApiService.UpdateCategoryAsync(categoryViewModel.Id, category);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryToDelete = await _categoryApiService.GetCategoryByIdAsync(id);
            if (categoryToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var category = _mapper.Map<CategoryViewModel>(categoryToDelete);

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _categoryApiService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
