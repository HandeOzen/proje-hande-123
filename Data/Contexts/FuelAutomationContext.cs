using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class FuelAutomationContext:DbContext


   
    {

        public FuelAutomationContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Tanks> Tanks  { get; set; }
        public DbSet<FuelTypes> FuelTypes  { get; set; }
        public DbSet<Sales> Sales  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // IConfigurationRoot configuration = new ConfigurationBuilder().GetConnectionString("MsSqlConnection");
            
            optionsBuilder.UseSqlServer("server=DESKTOP-149UQUB\\MSSQLSERVER5;database=FuelAutomation;integrated security=SSPI;");
            
        }

    }
}
