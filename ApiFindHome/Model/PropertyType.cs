using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class PropertyType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}