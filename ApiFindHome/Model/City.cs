using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiFindHome.Model
{
    public class City : Base
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Address> Addresses { get; set; }
        [Required]
        public Country Country { get; set; }
    }
}