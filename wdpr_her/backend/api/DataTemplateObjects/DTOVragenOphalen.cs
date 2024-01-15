using System.ComponentModel.DataAnnotations;

namespace api.DataTemplate;

public class DTOVragenOphalen
{
    public string? ID { get; set; }
    public string? Titel { get; set; }
    public string? UitvoerderNaam { get; set; }
    public string? Locatie { get; set; }
    public string? Beloning { get; set; }
    public string? Beschrijving { get; set; }
    public string? OnderzoeksData { get; set; }
}
