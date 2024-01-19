using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTOLogin
{
    [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
    public string? Gebruikersnaam { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht.")]
    public string? Wachtwoord { get; set; }
}