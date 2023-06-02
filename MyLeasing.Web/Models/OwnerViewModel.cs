using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MyLeasing.Web.Data.Entities;


namespace MyLeasing.Web.Models
{
    public class OwnerViewModel : Owner
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
