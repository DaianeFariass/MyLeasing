using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; } 
        public int Document { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Fixed Phone")]
        public int? FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public int CellPhone { get; set; }

        public string Address { get; set; }

        
    }
}
