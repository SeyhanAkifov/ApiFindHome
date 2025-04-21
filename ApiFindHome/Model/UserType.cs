using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class UserType : Base
    {
       
        [Required]
        public string Name { get; set; }
    }
}