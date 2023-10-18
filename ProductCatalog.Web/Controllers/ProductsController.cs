using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;

namespace ProductCatalog.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly ICategoryApiService _categoryApiService;
        private readonly ICurrencyRateApiService _currencyRateApiService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductApiService productApiService,
            ICategoryApiService categoryApiService,
            ICurrencyRateApiService currencyRateApiService,
            IMapper mapper)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _currencyRateApiService = currencyRateApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetProductsAsync();
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            var categories = await _categoryApiService.GetCategoriesAsync();
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            var usdRate = await _currencyRateApiService.GetUsdRateAsync();
            ViewBag.Cur_OfficialRate = usdRate.Cur_OfficialRate;
            ViewBag.Categories = categoriesViewModel;

            return View(productsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FilterProductViewModel filterProducts)
        {
            var filterProductsDto = _mapper.Map<FilterProductDto>(filterProducts);

            var products = await _productApiService.GetProductsAsync(filterProductsDto);
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            ViewBag.FilterProducts = filterProducts;

            var usdRate = await _currencyRateApiService.GetUsdRateAsync();
            ViewBag.Cur_OfficialRate = usdRate.Cur_OfficialRate;

            var categories = await _categoryApiService.GetCategoriesAsync();
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            ViewBag.Categories = categoriesViewModel;

            return View(productsViewModels);
        }

        [HttpGet("products/category/{categoryId}")]
        public async Task<IActionResult> ProductsByCategory(Guid categoryId)
        {
            var products = await _productApiService.GetProductsByCategoryIdAsync(categoryId);
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            return View(productsViewModels);
        }

        [Authorize(Roles = "AdvancedUser, User")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [Authorize(Roles = "AdvancedUser, User")]
        [HttpPost]
        public async Task<IActionResult> Create(UpdateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productDto = _mapper.Map<UpdateProductDto>(productViewModel);
                var result = await _productApiService.CreateProductAsync(productDto);

                if (result != null)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [Authorize(Roles = "AdvancedUser, User")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productToUpdate = await _productApiService.GetProductByIdAsync(id);
            if (productToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var categories = await _categoryApiService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var product = _mapper.Map<UpdateProductViewModel>(productToUpdate);

            return View(product);
        }

        [Authorize(Roles = "AdvancedUser, User")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var productDto = _mapper.Map<UpdateProductDto>(productViewModel);
                var result = await _productApiService.UpdateProductAsync(id, productDto);

                if (result != null)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [Authorize(Roles = "AdvancedUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productToDelete = await _productApiService.GetProductByIdAsync(id);
            if (productToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = _mapper.Map<ProductViewModel>(productToDelete);

            return View(product);
        }

        [Authorize(Roles = "AdvancedUser")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _productApiService.DeleteProductAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}