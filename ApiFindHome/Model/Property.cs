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

        public PropertyType Type { get; set; }

        public int Beds { get; set; }

        public int Baths { get; set; }

        public int Area { get; set; }

        public bool Garden { get; set; }

        public AdFor AdFor { get; set; }

        public int Floor { get; set; }

        public int YearOfConstruction { get; set; }

        public ApplicationUser Creator { get; set; }

        public DateTime AddedOn { get; set; }

        public Address Address { get; set; }

        public decimal Price { get; set; }
    }
}
