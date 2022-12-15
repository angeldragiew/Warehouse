using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Services.Contracts;
using Warehouse.Core.ViewModels;
using Warehouse.Infrastructure.Data.Models;
using Warehouse.Infrastructure.Data.Repo;

namespace Warehouse.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IRepository<Product> productRepo;

        public ProductService(IWebHostEnvironment hostEnvironment,
            IRepository<Product> productRepo)
        {
            this.hostEnvironment = hostEnvironment;
            this.productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductViewModel>> AllAsync(string category, string searchString)
        {
            var products = await productRepo
                .All()
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.ImageUrl,
                    Category = p.Category,
                    PurchasePrice = p.PurchasePrice,
                    SellingPrice = p.SellingPrice,
                    Description = p.Description,
                    Quantity = p.Quantity
                }).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(category))
            {
                products = products
                    .Where(p => p.Category.ToString().ToLower() == category.ToLower()).ToList();
            }
            return products;
        }

        public async Task CreateAsync(CreateProductViewModel model, string userId)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                PurchasePrice = model.PurchasePrice,
                SellingPrice = model.SellingPrice,
                Quantity = model.Quantity,
                ApplicationUserId = userId
            };

            if (model.Image != null)
            {
                string imageFileName = UploadFile(model.Image);
                product.ImageUrl = imageFileName;
            };

            await productRepo.AddAsync(product);
            await productRepo.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var productToDelete = await productRepo
                .All()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (productToDelete == null)
            {
                throw new ArgumentNullException("Unknown product!");
            }

            if (productToDelete.ImageUrl != null)
            {
                DeleteFile(productToDelete.ImageUrl);
            }

            productRepo.Delete(productToDelete);
            await productRepo.SaveChangesAsync();
        }

        public async Task<EditProductViewModel> GetByIdAsync(string id)
        {
            var product = await productRepo
               .All()
               .Select(p => new EditProductViewModel()
               {
                   Id = p.Id,
                   Name = p.Name,
                   Category = p.Category,
                   PurchasePrice = p.PurchasePrice,
                   SellingPrice = p.SellingPrice,
                   Description = p.Description,
                   Quantity = p.Quantity
               }).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new ArgumentNullException("Unknown product!");
            }

            return product;
        }

        public async Task EditAsync(EditProductViewModel model)
        {
            var productToUpdate = await productRepo
                .All()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (productToUpdate == null)
            {
                throw new ArgumentNullException("Unknown product!");
            }

            productToUpdate.Name = model.Name;
            productToUpdate.Description = model.Description;
            productToUpdate.SellingPrice = model.SellingPrice;
            productToUpdate.PurchasePrice = model.PurchasePrice;
            productToUpdate.Quantity = model.Quantity;
            productToUpdate.Category = model.Category;

            if (model.Image != null)
            {
                if (productToUpdate.ImageUrl != null)
                {
                    DeleteFile(productToUpdate.ImageUrl);
                }

                string imageFileName = UploadFile(model.Image);
                productToUpdate.ImageUrl = imageFileName;
            }

            await productRepo.SaveChangesAsync();
        }

        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(hostEnvironment.WebRootPath, "uploads");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        private void DeleteFile(string fileName)
        {
            var folderPath = Path.Combine(hostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
