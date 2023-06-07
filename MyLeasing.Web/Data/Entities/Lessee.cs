using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MyLeasing.Commom.Data.Entities;


namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        public string Address { get; set; }

        [Display(Name = "Photo")]
        public Guid ImageId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
        public string UserId { get; set; }
        public User user { get; set; }
        public string ImageFullPath => ImageId == Guid.Empty
             ? $"https://myleasingwebtpsi.azurewebsites.net/photos//imagemindisponivel.png"
             : $"https://myleasingwebtpsi.blob.core.windows.net/lessees/{ImageId}";


    }
}
