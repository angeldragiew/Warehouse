using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Core.ViewModels
{
    public class ProductsSearchViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public string? ProductId { get; set; }
        public string? ProductCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
