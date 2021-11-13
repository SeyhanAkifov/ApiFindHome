using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Dto
{
    public class HomePagePropertyDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Beds { get; set; }

        public int Baths { get; set; }

        public int Area { get; set; }

        public string PropertyType { get; set; }

        public string YearsAgo { get; set; }

        public string PostCode { get; set; }

        public string CityName { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string Condition { get; set; }
    }
}
