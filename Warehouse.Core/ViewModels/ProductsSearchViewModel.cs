using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Core.ViewModels
{
    public class ProductsSearchViewModel
    {
        public int PageNo { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public string? ProductId { get; set; }
        public string? ProductCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
