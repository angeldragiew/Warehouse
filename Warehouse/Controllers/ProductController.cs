using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Warehouse.Core.Services;
using Warehouse.Core.Services.Contracts;
using Warehouse.Core.ViewModels;
using Warehouse.Infrastructure.Data.Models;

namespace Warehouse.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductController(IProductService productService,
            UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await productService.AllAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await productService.Delete(id);
            }catch(Exception ex)
            {
                //TODO:
            }
            return RedirectToAction("All", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await productService.CreateAsync(model, userId);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("All", "Product");
        }
    }
}
