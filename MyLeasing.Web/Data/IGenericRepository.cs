using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Commom.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync (T entity);

        Task UpdateAsync (T entity);

        Task DeleteAsync (T entity);

        Task<bool> ExistAsync (T entity);
    }
}
