using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Commom.Data
{
    public class OwnerRepository : GenericRepository<Owner>,  IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(p => p.User);
        }
        public async Task<Owner> GetOwnerByIdWithUserAsync(int? id)
        {
            return await Task.Run(() =>
            {
                return _context.Owners.Include(o => o.User).FirstOrDefault(o => o.Id == id);
            });
        }

    }
}
