using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class PropertyType : Base
    {
      
        [Required]
        public string Name { get; set; }
    }
}