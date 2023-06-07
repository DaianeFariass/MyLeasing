using System;
using System.Net;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;


namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                OwnerName = model.OwnerName,
                Document = model.Document,
                CellPhone = model.CellPhone,
                FixedPhone = model.FixedPhone,
                Address = model.Address,
                ImageId = imageId,
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
                ImageId = owner.ImageId,
                UserId= owner.UserId,
                User = owner.User
            };
        }
        public Lessee ToLesse(LesseeViewModel model, Guid imageId, bool isNew) 
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                ImageId = imageId,
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
                ImageId = lessee.ImageId,
                Address = lessee.Address,
                UserId= lessee.UserId,
                user = lessee.user
                
            };

        }
    }
}
