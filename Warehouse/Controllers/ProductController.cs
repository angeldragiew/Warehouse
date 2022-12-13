using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Warehouse.Core.Constants;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var product = await productService.GetByIdAsync(id);
                return View(product);
            }
            catch (ArgumentNullException ex)
            {
                TempData[MessageConstant.ErrorMessage] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await productService.EditAsync(model);
                TempData[MessageConstant.SuccessMessage] = "Product successfully updated!";
            }
            catch (ArgumentNullException ex)
            {
                TempData[MessageConstant.ErrorMessage] = ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await productService.Delete(id);
                TempData[MessageConstant.SuccessMessage] = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = ex.Message;
            }
            return RedirectToAction("Index", "Home");
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
                TempData[MessageConstant.SuccessMessage] = "Product created successfully!";
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
