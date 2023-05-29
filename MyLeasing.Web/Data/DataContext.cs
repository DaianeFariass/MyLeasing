
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data.Entities;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
   
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Owner> Owners { get; set; }
      
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Owner)
                .WithOne(o => o.User)
                .HasForeignKey<Owner>(u => u.UserId);
        }
    }
}
