using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public ICollection<Address> Addresses { get; set; }
        [Required]
        public Country Country { get; set; }
    }
}