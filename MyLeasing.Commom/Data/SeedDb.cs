using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Commom.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;
        public SeedDb(DataContext context)
        {
           _context = context;
            _random = new Random();
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if(!_context.Owners.Any()) 
            {
                AddOwner("Carmelita Alves");
                AddOwner("Cecília Borba");
                AddOwner("Daiane Farias");
                AddOwner("Elisangela Dias");
                AddOwner("Francisco Bezerra");
                AddOwner("Olívia Pires");
                AddOwner("Reinaldo Bezerra");
                AddOwner("Renan Oliveira");
                AddOwner("Romeu Silva");
                AddOwner("Rafael Santos");
                await _context.SaveChangesAsync();
            }

        }

        private void AddOwner(string name)
        {
            int prefix = _random.Next(1000, 1000);
            int lineNumber = _random.Next(1000, 10000);
            string  phoneNumber = $"{prefix}{lineNumber}";
            string[] streetSuffixes = { "Rua", "Avenida", "Praceta"};
            string[] streetNames = { "José", "Brasil", "Angola", "Carlos", "Rodrigues", "Tody", "Santos", "Silva", "Conceição", "Teles" };
            string number = _random.Next(1, 1000).ToString();
            string streetName = streetNames[_random.Next(streetNames.Length)];
            string streetSuffix = streetSuffixes[_random.Next(streetSuffixes.Length)];
            string address = $"{streetSuffix}: {streetName}, {number}";

            _context.Owners.Add(new Owner
            {
                Document = _random.Next(10000, 100000),
                OwnerName = name,
                FixedPhone = _random.Next(10000,100000),
                CellPhone = _random.Next(10000,100000),
                Address = address

            }); 
            
        }
    }
}
