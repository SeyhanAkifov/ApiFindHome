using ApiFindHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Dto
{
    public class PropertyInputModelDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Creator { get; set; }
        public string AdFor { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Beds { get; set; }
        public int Floor { get; set; }
        public int Baths { get; set; }
        public string Condition { get; set; }
        public string ImageUrl { get; set; }
        public int YearOfConstruction { get; set; }
        public int Area { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public decimal Price { get; set; }
        public bool Garden { get; set; }
        public Feature Feature { get; set; }
      
    }
}
