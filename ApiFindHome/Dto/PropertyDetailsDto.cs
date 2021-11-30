using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Dto
{
    public class PropertyDetailsDto
    {
        
        public int Id { get; set; }
        
        public string Type { get; set; }

        public int Beds { get; set; }

        public int Baths { get; set; }
        
        public int Area { get; set; }
        
        public bool Garden { get; set; }
        
        public string AdFor { get; set; }
        
        public int Floor { get; set; }
        
        public string Condition { get; set; }
        
        public string Description { get; set; }
        
        public int YearOfConstruction { get; set; }
        
        public string Creator { get; set; }
        
        public DateTime AddedOn { get; set; }

        public string PostCode { get; set; }
        
        public string StreetName { get; set; }
        
        public string StreetNumber { get; set; }
       
        public string City { get; set; }

        public string Country { get; set; }

        public decimal Price { get; set; }

        public string YearsAgo { get; set; }

        public List<string> Feature { get; set; }
    }
}
