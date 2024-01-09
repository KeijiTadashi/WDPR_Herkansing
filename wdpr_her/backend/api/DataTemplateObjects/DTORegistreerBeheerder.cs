using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTORegistreerBeheerder : DTORegistreer
{
    [Required(ErrorMessage = "Voornaam is verplicht.")]
    public string Voornaam { get; set; }
    
    [Required(ErrorMessage = "Achternaam is verplicht.")]
    public string Acternaam { get; set; }
    
}