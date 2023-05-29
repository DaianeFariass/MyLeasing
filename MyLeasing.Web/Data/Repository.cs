using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using Newtonsoft.Json.Bson;


namespace MyLeasing.Commom.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(p => p.OwnerName);
        }
        public Owner GetOwner(int id)
        {
            return _context.Owners.Find(id);
        }
        public void AddOwner(Owner owner)
        {
            _context.Owners.Add(owner);
        }
        public void UpdaterOwner(Owner owner)
        {
            _context.Owners.Update(owner);
        }
        public void RemoveOwner(Owner owner)
        {
            _context.Owners.Remove(owner);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //Gravar tudo que está pendente em memória 
        }
        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(p => p.Id == id);
        }
    }
}
