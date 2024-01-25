namespace api.DataTemplate;

public class DTOGetOnderzoek
{
    public int OnderzoekId { get; set; }
    public string Titel { get; set; }
    public string UitvoerderId { get; set; }
    public string UitvoerderNaam { get; set; } // Beheerder of Bedrijf
    public string? Locatie { get; set; }
    public string? Beloning { get; set; }
    public List<int> OnderzoeksType { get; set; }
    public string Beschrijving { get; set; }
    public string OnderzoeksData { get; set; } // Format van vragen etc in JSON format
}