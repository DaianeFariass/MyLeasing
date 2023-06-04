using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data;
using MyLeasing.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class LesseeRepository : GenericRepository<Lessee>, ILesseeRepository
    {
        private readonly DataContext _context;

        public LesseeRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
        public IQueryable GetAllWithUser()
        {
            return _context.Lessee.Include(p => p.user);
        }


        public async Task<Lessee> GetLesseeByIdWithUserAsync(int? id)
        {
            return await Task.Run(() =>
            {
                return _context.Lessee.Include(o => o.user).FirstOrDefault(o => o.Id == id);
            });
        }
    }
}
