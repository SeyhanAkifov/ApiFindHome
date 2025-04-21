using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public Address Address { get; set; }

        public UserType UserType { get; set; }

        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Language { get; set; }
        public string AboutMe { get; set; }

        #region Social Media
        public string Skype { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Pinterest { get; set; }
        public byte[] Photo { get; set; }
        #endregion 

    }
}