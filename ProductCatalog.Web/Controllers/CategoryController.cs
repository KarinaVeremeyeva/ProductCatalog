using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
