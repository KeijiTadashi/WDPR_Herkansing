namespace api.DataTemplate;

public class DTOCreateOnderzoek
{
    public string Titel { get; set; }
    public string? Locatie { get; set; }
    public string? Beloning { get; set; }
    public List<int>? OnderzoeksTypes { get; set; }
    public string Beschrijving { get; set; }
    public string OnderzoeksData { get; set; } // Format van vragen etc in JSON format
}