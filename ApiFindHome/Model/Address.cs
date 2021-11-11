using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
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