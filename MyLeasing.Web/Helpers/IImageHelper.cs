using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
        Task DeleteImageAsync(string imageUrl);
    }
}
