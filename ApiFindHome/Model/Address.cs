using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class Address : Base
    {
        [Required]
        public string PostCode { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        public City City { get; set; }
    }
}