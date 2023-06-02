using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

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
                User = owner.User
            };
        }
    }
}
