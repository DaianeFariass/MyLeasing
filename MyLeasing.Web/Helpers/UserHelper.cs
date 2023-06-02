using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<User> CreateUserAsync(string name, string email, string password, string cellPhone, string document, string address)
        {
            var user = new User
            {
                FirstName = name.Split(' ').FirstOrDefault(),
                LastName = name.Split(' ').Skip(1).FirstOrDefault(),
                Email = email,
                UserName = email,
                PhoneNumber = cellPhone,
                Document = document,
                Address = address
            };

            var result = await AddUserAsync(user, password);
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<IdentityResult> UpdateUserAsync(User user, string name, string address, string phonenumber, string document)
        {
            user.FirstName = name.Split(" ").FirstOrDefault();
            user.LastName = name.Split(" ").Skip(1).FirstOrDefault();
            user.Address = address;
            user.PhoneNumber = phonenumber;
            user.Document = document;
            return await _userManager.UpdateAsync(user);

        }
        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}
