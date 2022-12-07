using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Enums;

namespace Warehouse.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 50 symbols")]
        public string Name { get; set; }


        [StringLength(2000, ErrorMessage = "Product description must be up to 2000 symbols")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }

        public ProductCategory Category { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
