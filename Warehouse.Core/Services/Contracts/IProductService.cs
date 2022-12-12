using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.ViewModels;

namespace Warehouse.Core.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> AllAsync();
        Task CreateAsync(CreateProductViewModel model, string userId);
    }
}
