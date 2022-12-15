using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Warehouse.Core.Constants;
using Warehouse.Core.Services;
using Warehouse.Core.Services.Contracts;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string category, string searchString)
        {

            var products = await productService.AllAsync(category, searchString);
            return View(products);
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