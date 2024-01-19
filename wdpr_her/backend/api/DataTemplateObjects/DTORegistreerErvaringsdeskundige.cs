using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTORegistreerErvaringsdeskundige : DTORegistreer
{
    [Required(ErrorMessage = "Voornaam is verplicht.")]
    public string Voornaam { get; set; }

    [Required(ErrorMessage = "Achternaam is verplicht.")]
    public string Achternaam { get; set; }

    [Required(ErrorMessage = "Postcode is verplicht.")]
    public string Postcode { get; set; }
}