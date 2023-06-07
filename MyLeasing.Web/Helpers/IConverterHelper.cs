using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew);

        OwnerViewModel ToOwnerViewModel(Owner owner);

        Lessee ToLesse(LesseeViewModel model, Guid imageId, bool isNew);

        LesseeViewModel ToLesseeViewModel(Lessee lessee);
    }
}
