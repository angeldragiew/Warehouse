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
        Task<ProductsSearchViewModel> AllAsync(string productId, string category, string searchString);
        Task CreateAsync(CreateProductViewModel model, string userId);

        Task Delete(string id);

        Task<EditProductViewModel> GetByIdAsync(string id);

        Task EditAsync(EditProductViewModel model);
    }
}
