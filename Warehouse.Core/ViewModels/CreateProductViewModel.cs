using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.CustomAttributes;
using Warehouse.Infrastructure.Enums;

namespace Warehouse.Core.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }


        [StringLength(2000)]
        [Display(Name = "Product Description")]
        public string? Description { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [Display(Name= "Product Image")]
        public IFormFile? Image { get; set; }


        [Display(Name = "Product Sell Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal SellingPrice { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Product Purchase Price")]
        public decimal PurchasePrice { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }


        [Display(Name="Product Category")]
        public ProductCategory Category { get; set; }

    }
}
