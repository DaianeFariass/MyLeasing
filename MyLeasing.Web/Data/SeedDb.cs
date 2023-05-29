using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MyLeasing.Commom.Data.Entities;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLeasing.Commom.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
       
        private Random _random;       
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;          
            _random = new Random();
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("daia_crica@hotmail.com");

            if (user == null) 
            {

                user = new User
                {
                    FirstName = "Daiane",
                    LastName = "Farias",
                    UserName = "daia_crica@hotmail.com",
                    Document = GenerateRandomNumbers(6),
                    Address = GenerateRandomAddress(),
                    Email = "daia_crica@hotmail.com",
                    PhoneNumber = GenerateRandomNumbers(6),
                    
                   
                };
                var owner = new Owner
                {                
                    Document = user.Document,
                    OwnerName = user.FirstName + " " + user.LastName,
                    FixedPhone = user.PhoneNumber,
                    CellPhone = user.PhoneNumber,
                    Address = user.Address,           
                    UserId = user.Id,
                    User = user
                };
                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user");
                }
                _context.Owners.Add(owner);
            }
            await _context.SaveChangesAsync();
        }
      

        private void AddOwner(string name)
        {
            _context.Owners.Add(new Owner
            {
                OwnerName = name,
                Document = GenerateRandomNumbers(6),
                FixedPhone = GenerateRandomNumbers(9),
                CellPhone = GenerateRandomNumbers(9),
                Address = GenerateRandomAddress(),
              
            });
        }
        private string GenerateRandomNumbers(int value)
        {
            string phoneNumber = "";
            for (int i = 0; i < value; i++)
            {
                phoneNumber += _random.Next(10).ToString();
            }
            return phoneNumber;
        }

        private string GenerateRandomAddress()
        {
            string[] streetSuffixes = { "Rua", "Avenida", "Praceta", "Calçada" };
            string[] streets = { "José", "Brasil", "Angola", "Carlos", "Rodrigues", "Tody", "Santos", "Silva", "Conceição", "Teles" };
            string number = _random.Next(1, 1000).ToString();
            string streetSuffix = streetSuffixes[_random.Next(streetSuffixes.Length)];
            string street = streets[_random.Next(streets.Length)];

            return $" {streetSuffix}: {street}, {number}";
        }
      
    } 
}
