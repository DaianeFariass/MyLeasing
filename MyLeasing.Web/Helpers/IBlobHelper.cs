using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;


namespace MyLeasing.Web.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);

        Task<Guid> UploadBlobAsync(byte[] file, string containerName);

        Task<Guid> UploadBlobAsync(string image, string containerName);

        Task DeleteImageAsync(string image);
    }
}
