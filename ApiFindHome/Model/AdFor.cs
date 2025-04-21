using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model;

public class AdFor : Base
{
    [Required]
    public string Name { get; set; }
}
