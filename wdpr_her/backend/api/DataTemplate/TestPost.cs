using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class TestPost
{
    [Required(ErrorMessage = "Username is required")]
    public string Name { get; init; }

    [Required(ErrorMessage = "is required")]
    public bool Ditiseentestbool { get; init; } 
}