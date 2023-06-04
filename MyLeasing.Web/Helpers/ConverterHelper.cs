using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System.Net;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner ToOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                OwnerName = model.OwnerName,
                Document = model.Document,
                CellPhone = model.CellPhone,
                FixedPhone = model.FixedPhone,
                Address = model.Address,
                ImageUrl = path,
                UserId= model.UserId,
                User = model.User,
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                OwnerName = owner.OwnerName,
                Document = owner.Document,
                CellPhone = owner.CellPhone,
                FixedPhone = owner.FixedPhone,
                Address = owner.Address,
                ImageUrl = owner.ImageUrl,
                UserId= owner.UserId,
                User = owner.User
            };
        }
        public Lessee ToLesse(LesseeViewModel model, string path, bool isNew) 
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                ImageUrl = path,
                Address = model.Address,
                UserId = model.UserId,
                user = model.user
              

            };
                  
        }
        public LesseeViewModel ToLesseeViewModel(Lessee lessee) 
        {
            return new LesseeViewModel
            {
                Id = lessee.Id,
                Document = lessee.Document,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                ImageUrl = lessee.ImageUrl,
                Address = lessee.Address,
                UserId= lessee.UserId,
                user = lessee.user
                
            };

        }
    }
}
