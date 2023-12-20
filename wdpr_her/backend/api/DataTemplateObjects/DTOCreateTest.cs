using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTOCreateTest
{
    [Required(ErrorMessage = "Username is required")]
    public string Name { get; init; }

    [Required(ErrorMessage = "is required")]
    public bool DitIsEenTestBool { get; init; } 
}