using MyLeasing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeasing.Commom.Data
{
    public class MockRepository : IRepository
    {
        public void AddOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Owner GetOwner(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Owner> GetOwners()
        {
            var owners = new List<Owner>();
            owners.Add(new Owner { Id = 1, OwnerName = "Um", CellPhone = 123456, Address= "1" });
            owners.Add(new Owner { Id = 2, OwnerName = "Dois", CellPhone = 654321, Address = "2" });
            owners.Add(new Owner { Id = 3, OwnerName = "Três", CellPhone = 789456, Address = "3" });
            owners.Add(new Owner { Id = 4, OwnerName = "Quatro", CellPhone = 456789, Address = "4" });
            owners.Add(new Owner { Id = 5, OwnerName = "Cinco", CellPhone = 0231456, Address = "5" });

            return owners;
        }

        public bool OwnerExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdaterOwner(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
