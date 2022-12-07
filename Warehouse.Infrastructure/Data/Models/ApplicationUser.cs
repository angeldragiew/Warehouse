using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
