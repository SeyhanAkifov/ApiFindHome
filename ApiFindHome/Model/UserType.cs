using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}