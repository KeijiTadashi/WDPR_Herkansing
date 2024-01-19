using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTORegistreer
{
    [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
    public string Gebruikersnaam { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht.")]
    public string Wachtwoord { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is verplicht.")]
    public string Email { get; set; }

    public string Telefoonnummer { get; set; }
}