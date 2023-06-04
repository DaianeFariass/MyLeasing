using MyLeasing.Commom.Data;
using MyLeasing.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public interface ILesseeRepository : IGenericRepository<Lessee>
    {
        public IQueryable GetAllWithUser();

        public Task<Lessee> GetLesseeByIdWithUserAsync(int? id);
    }
}
