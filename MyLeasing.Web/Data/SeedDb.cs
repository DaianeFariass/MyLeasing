using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;


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

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lesse");

            var userAdmin = await _userHelper.GetUserByEmailAsync("daiane.farias@cinel.pt");

            if (userAdmin == null)
            {
                userAdmin = new User
                {
                    FirstName = "Daiane",
                    LastName = "Farias",
                    UserName = "daiane.farias@cinel.pt",
                    Document = GenerateRandomNumbers(9),
                    Address = GenerateRandomAddress(),
                    Email = "daiane.farias@cinel.pt",
                    PhoneNumber = GenerateRandomNumbers(6),
                };
                var result = await _userHelper.AddUserAsync(userAdmin, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userAdmin, "Admin");

            }
            var userOwner = await _userHelper.GetUserByEmailAsync("reinaldopires@cinel.pt");

            if (userOwner == null)
            {
                userOwner = new User
                {
                    FirstName = "Reinaldo",
                    LastName = "Pires",
                    UserName = "reinaldopires@cinel.pt",
                    Document = GenerateRandomNumbers(9),
                    Address = GenerateRandomAddress(),
                    Email = "reinaldopires@cinel.pt",
                    PhoneNumber = GenerateRandomNumbers(9),
                };
                var result = await _userHelper.AddUserAsync(userOwner, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await _userHelper.AddUserToRoleAsync(userOwner, "Owner");

            }
            var userLesse = await _userHelper.GetUserByEmailAsync("oliviaborba@cinel.pt");

            if (userLesse == null)
            {
                userLesse = new User
                {
                    FirstName = "Olivia",
                    LastName = "Borba",
                    UserName = "oliviaborba@cinel.pt",
                    Document = GenerateRandomNumbers(9),
                    Address = GenerateRandomAddress(),
                    Email = "oliviaborba@cinel.pt",
                    PhoneNumber = GenerateRandomNumbers(9),
                };
                var result = await _userHelper.AddUserAsync(userLesse, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await _userHelper.AddUserToRoleAsync(userLesse, "Lesse");

            }
            var isInRole = await _userHelper.IsUserInRoleAsync(userAdmin, "Admin");
            var isInRoleOwner = await _userHelper.IsUserInRoleAsync(userOwner, "Owner");
            var isInRoleLesse = await _userHelper.IsUserInRoleAsync(userLesse, "Lesse");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(userAdmin, "Admin");

            }
            if (!isInRoleOwner)
            {

                await _userHelper.AddUserToRoleAsync(userOwner, "Owner");

            }
            if (!isInRoleLesse)
            {

                await _userHelper.AddUserToRoleAsync(userLesse, "Lesse");
            }
            if (!_context.Owners.Any())
            {
                AddOwner("Ana Martins", userOwner);
                AddOwner("Bianca Andrade", userOwner);
                AddOwner("Maria Alves", userOwner);
                AddOwner("Luana Piovani", userOwner);
                await _context.SaveChangesAsync();
            }
            if (!_context.Lessee.Any())
            {
                AddLesse("Leonor Santos", userLesse);
                AddLesse("Romeu Teles", userLesse);
                AddLesse("Santiago Rosa", userLesse);
                AddLesse("Thamos Silva", userLesse);
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string name, User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = GenerateRandomNumbers(9),
                OwnerName = name,
                FixedPhone = GenerateRandomNumbers(9),
                CellPhone = GenerateRandomNumbers(9),
                Address = GenerateRandomAddress(),
                UserId = user.Id,
                User = user
            });

        }
        private void AddLesse(string name, User user)
        {
            _context.Lessee.Add(new Lessee
            {
                Document = GenerateRandomNumbers(9),
                FirstName = name.Split(" ").FirstOrDefault(),
                LastName = name.Split(" ").Skip(1).FirstOrDefault(),
                FixedPhone = GenerateRandomNumbers(9),
                CellPhone = GenerateRandomNumbers(9),
                Address = GenerateRandomAddress(),
                UserId = user.Id,
                user = user
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
