using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiFindHome.Model
{
    public class Country : Base
    {
        
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<City> Cities { get; set; }
    }
}