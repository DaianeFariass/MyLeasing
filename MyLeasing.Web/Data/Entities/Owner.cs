using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MyLeasing.Commom.Data.Entities;


namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity
    {
        public int Id { get; set; } 
        public string Document { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        public string Address { get; set; }

        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
             ? $"https://myleasingwebtpsi.azurewebsites.net/photos//imagemindisponivel.png"
             : $"https://myleasingwebtpsi.blob.core.windows.net/owners/{ImageId}";

    }
}
