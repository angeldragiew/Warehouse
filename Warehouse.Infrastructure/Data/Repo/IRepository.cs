using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.Data.Repo
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> All();

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();
    }
}
