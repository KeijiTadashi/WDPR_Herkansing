using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTORegistreerBedrijf : DTORegistreer
{
    [Required(ErrorMessage = "Bedrijfsnaam is verplicht.")]
    public string Bedrijfsnaam { get; set; }
    [Required(ErrorMessage = "Kvk nummer is verplicht.")]
    public long Kvk { get; set; }
    public string? Website { get; set; }
    public string? Locatie { get; set; }
}