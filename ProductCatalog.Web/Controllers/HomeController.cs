﻿using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Web.Models;
using System.Diagnostics;

namespace ProductCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
