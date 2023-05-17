using Microsoft.AspNetCore.Identity;

namespace FuelAutomation.Identity
{
    public class Roles:IdentityRole<int>
    {
        public DateTimeOffset CreatedOn { get; set; }
    }
}
