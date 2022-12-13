using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Enums;

namespace Warehouse.Core.ViewModels
{
    public class EditProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public ProductCategory Category { get; set; }
    }
}
