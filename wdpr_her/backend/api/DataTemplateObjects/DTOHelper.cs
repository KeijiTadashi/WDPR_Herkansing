using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTOHelper
{
    [Required(ErrorMessage = "Naam/type is verplicht.")]
    public string? Naam { get; set; }
}