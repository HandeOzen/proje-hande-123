

using Data.Seed;
using Entity;
using FuelAutomation.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class ApplicationDbContext:IdentityDbContext<Users>

    {
        public DbSet<Tanks> Tanks { get; set; }
        public DbSet<FuelTypes> FuelTypes { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options):base(options)
        
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // IConfigurationRoot configuration = new ConfigurationBuilder().GetConnectionString("MsSqlConnection");

                optionsBuilder.UseSqlServer("server=DESKTOP-149UQUB\\MSSQLSERVER5;database=FuelAutomation;integrated security=SSPI;");

            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            new DbInitializer(builder).Seed();
            base.OnModelCreating(builder);
           
        }

    }
}
