using Entity;
using FuelAutomation.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seed
{
    public class DbInitializer
    {


        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }


        public void Seed()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<Users>().HasData(
                   new Users()
                   {
                       Id= "8e445865-a24d-4543-a6c6-9443d048cdb9",
                       UserName = "hande",
                       Email = "hande@gmail.com",
                       NormalizedEmail= "HANDE@GMAIL.COM",
                       FirstName = "hande",
                       LastName = "özen",
                       EmailConfirmed = true,
                       PasswordHash = hasher.HashPassword(null, "Aa123."),
                       

                   },
                   new Users()
                   {
                       Id= "2af31ffc-1634-4ca7-8ebb-48adb0344657",
                       UserName = "hande2",
                       Email = "hande2@gmail.com",
                       NormalizedEmail= "HANDE2@GMAIL.COM",
                       FirstName = "hande",
                       LastName = "özen",
                       EmailConfirmed = true,
                       PasswordHash=hasher.HashPassword(null,"Aa123.")
        }

                   );


            modelBuilder.Entity<IdentityRole>().HasData(

                new IdentityRole()
                {

                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Admin",
                    NormalizedName="ADMIN"
                },
                  new IdentityRole()
                  {

                      Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                      Name = "Staff",
                      NormalizedName = "STAFF"
                  }
                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
           new IdentityUserRole<string>
           {
               RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
               UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
               
           } ,
           new IdentityUserRole<string>
           {
               RoleId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
               UserId = "2af31ffc-1634-4ca7-8ebb-48adb0344657",
               
           }
           );

            modelBuilder.Entity<FuelTypes>().HasData(


                new FuelTypes()
                {
                    Id=1,
                   Name="Benzin",
                   Price=20,


                },

                new FuelTypes()
                {
                    Id = 2,
                    Name = "LPG",
                    Price = 15,


                },

                new FuelTypes()
                {
                    Id = 3,
                    Name = "Motorin",
                    Price = 20,


                }



                );
            modelBuilder.Entity<Tanks>().HasData(


             new Tanks()
             { Id=1,
             Capacity=1000,
             Quantity=200,
            FuelTypesId=1,

            },
               new Tanks()
               {
                   Id = 2,
                   Capacity = 1000,
                   Quantity = 300,
                   FuelTypesId = 1,

               },
                new Tanks()
                {
                    Id = 3,
                    Capacity = 1000,
                    Quantity = 250,
                    FuelTypesId = 2,

                },
                 new Tanks()
                 {
                     Id = 4,
                     Capacity = 1000,
                     Quantity = 100,
                     FuelTypesId = 2,

                 },
                  new Tanks()
                  {
                      Id = 5,
                      Capacity = 1000,
                      Quantity = 300,
                      FuelTypesId = 3,

                  },
                   new Tanks()
                   {
                       Id = 6,
                       Capacity = 1000,
                       Quantity = 500,
                       FuelTypesId = 3,

                   }





                );
        }
    }
}
