using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.Models;
using ProductCatalog.Web.Services;
using ProductCatalog.Web.ViewModels;
using System.Diagnostics;

namespace ProductCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly IMapper _mapper;

        public HomeController(
            IProductApiService productApiService,
            IMapper mapper)
        {
            _productApiService = productApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetProductsAsync();
            var productsViewModels = _mapper.Map<List<ProductViewModel>>(products);
            
            return View(productsViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}