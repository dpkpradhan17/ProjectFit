using Microsoft.AspNetCore.Identity;

namespace ProjectFit.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryCode { get; set; }
        public long MobileNumber { get; set; }

    }
}
