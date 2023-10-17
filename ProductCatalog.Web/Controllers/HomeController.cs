using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Web.DTOs;
using ProductCatalog.Web.Models;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;
using System.Diagnostics;

namespace ProductCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly ICategoryApiService _categoryApiService;
        private readonly IMapper _mapper;

        public HomeController(
            IProductApiService productApiService,
            ICategoryApiService categoryApiService,
            IMapper mapper)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetProductsAsync();
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);
            
            return View(productsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FilterProductViewModel filterProducts)
        {
            var filterProductsDto = _mapper.Map<FilterProductDto>(filterProducts);

            var products = await _productApiService.GetProductsAsync(filterProductsDto);
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);

            ViewBag.FilterProducts = filterProducts;

            return View(productsViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryApiService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _productApiService.DeleteProductAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}