using System.Collections.Generic;
using System.Threading.Tasks;
using MyLeasing.Web.Data.Entities;


namespace MyLeasing.Commom.Data
{
    public interface IRepository
    {
        void AddOwner(Owner owner);

        Owner GetOwner(int id);

        IEnumerable<Owner> GetOwners();

        bool OwnerExists(int id);

        void RemoveOwner(Owner owner);

        Task<bool> SaveAllAsync();

        void UpdaterOwner(Owner owner);
    }
}