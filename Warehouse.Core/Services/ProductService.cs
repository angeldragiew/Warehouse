using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ProductViewModel>> AllAsync()
        {
            return await productRepo
                .All()
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.ImageUrl
                }).ToListAsync();
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
    }
}
