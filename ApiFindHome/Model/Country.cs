using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}