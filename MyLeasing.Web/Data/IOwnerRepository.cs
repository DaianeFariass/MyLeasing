
using MyLeasing.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Commom.Data
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        public IQueryable GetAllWithUsers();

        public Task<Owner> GetOwnerByIdWithUserAsync(int? id);
    }
}
