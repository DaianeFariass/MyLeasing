using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data.Entities;
using MyLeasing.Web.Data;


namespace MyLeasing.Commom.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context= context;
        }
        public IQueryable<T> GetAll() 
        {
            return _context.Set<T>().AsNoTracking(); // Vai a tabela busca o que precisa buscar e desliga da tabela
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task CreateAsync (T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();

        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();

        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();

        }
        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }

        public Task<bool> ExistAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
