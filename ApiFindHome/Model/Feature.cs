using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Model
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool AirConditioning { get; set; }
        [Required]
        public bool Barbeque { get; set; }
        [Required]
        public bool Dryer { get; set; }
        [Required]
        public bool Gym { get; set; }
        [Required]
        public bool Laundry { get; set; }
        [Required]
        public bool Lawn { get; set; }
        [Required]
        public bool Kitchen { get; set; }
        [Required]
        public bool OutdoorShower { get; set; }
        [Required]
        public bool Refrigerator { get; set; }
        [Required]
        public bool Sauna { get; set; }
        [Required]
        public bool SwimmingPool { get; set; }
        [Required]
        public bool TvCable { get; set; }
        [Required]
        public bool Washer { get; set; }
        [Required]
        public bool Wifi { get; set; }
        [Required]
        public bool WindowCoverings { get; set; }
    }
}
