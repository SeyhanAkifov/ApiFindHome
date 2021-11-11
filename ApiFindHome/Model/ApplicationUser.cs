using Microsoft.AspNetCore.Identity;

namespace ApiFindHome.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }

        public UserType UserType { get; set; }
    }
}