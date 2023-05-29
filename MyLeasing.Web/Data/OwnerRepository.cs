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
        public OwnerRepository(DataContext context) : base(context) 
        { 

        
        }
        
    }
}
