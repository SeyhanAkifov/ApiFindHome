using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string PostCode { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public City City { get; set; }


    }
}