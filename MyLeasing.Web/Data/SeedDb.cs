using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MyLeasing.Commom.Data.Entities;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLeasing.Commom.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ILesseeRepository _lesseeRepository;
        private Random _random;       
        public SeedDb(DataContext context, IUserHelper userHelper, IOwnerRepository ownerRepository, ILesseeRepository lesseeRepository)
        {
            _context = context;
            _userHelper = userHelper; 
            _ownerRepository = ownerRepository;
            _lesseeRepository = lesseeRepository;
            _random = new Random();
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            for (int i = 0; i < 5; i++)
            {

                User user = await GenerateUserAsync();
                await AddOwner(user);
            }

            for (int i = 0; i < 5; i++)
            {

                User user = await GenerateUserAsync();
                await AddLessee(user);
            }
            await _context.SaveChangesAsync();

            var getUser = await _userHelper.GetUserByEmailAsync("daia_crica@hotmail.com");          
        }
        private async Task<User> GenerateUserAsync()
        {
            var name = GenerateRandomFirstName();
            var email = GenerateRandomEmail(name);
            var user = new User
            {
                FirstName = GenerateRandomFirstName(),
                LastName = GenerateRandomLastName(),
                UserName = email,
                Document = GenerateRandomNumbers(6),
                Address = GenerateRandomAddress(),
                Email = email,
                PhoneNumber = GenerateRandomNumbers(6),

            };

            var result = await _userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user");
            }
            return user;
        }
        private async Task AddOwner(User user)
        {
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
            await _ownerRepository.CreateAsync(owner);
        }

        private async Task AddLessee(User user)
        {
            var lesse = new Lessee
            {
               Document = user.Document,
               FirstName= GenerateRandomFirstName(),
               LastName= GenerateRandomLastName(),
               FixedPhone= user.PhoneNumber,
               CellPhone= user.PhoneNumber, 
               Address = GenerateRandomAddress(),

            };

            await _lesseeRepository.CreateAsync(lesse);

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

        private string GenerateRandomFirstName()
        {
            string[] firstNames = { "Ana", "Clara", "Carmelita", "Cecilia", "Davi", "Olívia", "Reinaldo", "Renan", "Rafael", "Daiane" };

            return firstNames[_random.Next(firstNames.Length)];
        }
        private string GenerateRandomLastName()
        {
            string[] lastNames = { "Alves", "Farias", "Teles", "Borba", "Bezerra", "Pires", "Dias", "Souza", "Martins", "Silva" };

            return lastNames[_random.Next(lastNames.Length)];
        }
       private string GenerateRandomEmail(string email)
        {
            string[] domains = { "gmail.com", "hotmail.com", "Yahoo.com" };
            string randomString = Guid.NewGuid().ToString().Substring(0, 8);

            // Concatenando com o domínio
            email = randomString + "@" + _random.Next(domains.Length);

            return email;
        }

    } 
}
