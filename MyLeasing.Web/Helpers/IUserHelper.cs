using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(string id);
        Task<User> CreateUserAsync(string name, string email, string password, string document,string phoneNumber, string address);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user, string name, string document, string phoneNumber,string address);
        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
