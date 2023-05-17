using Microsoft.AspNetCore.Identity;

namespace FuelAutomation.Entity
{
    public class Users:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}





