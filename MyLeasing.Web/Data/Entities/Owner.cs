using MyLeasing.Commom.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ImageFullPath 
        { 
            get
            {
                if(string.IsNullOrEmpty(ImageUrl)) 
                {
                    return null;
                }
                return $"https://localhost:44307{ImageUrl.Substring(1)}";
         
            }
            
        
        }

    }
}
