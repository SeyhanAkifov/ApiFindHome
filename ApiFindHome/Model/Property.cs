using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Model
{
    public class Property
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PropertyType Type { get; set; }
        [Required]
        public int Beds { get; set; }
        [Required]
        public string Title { get; set; }

        public int Baths { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public bool Garden { get; set; }
        [Required]
        public AdFor AdFor { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int YearOfConstruction { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Feature Feature { get; set; }


    }
}
