namespace api;

public class Onderzoek
{
    public string Id { get; init; }
    public string Titel { get; set; }
    public Gebruiker Uitvoerder { get; init; } // Beheerder of Bedrijf
    public string? Locatie { get; set; }
    public string? Beloning { get; set; }
    public List<OnderzoeksType> OnderzoeksType { get; set; }
    public string Beschrijving { get; set; }
    public string OnderzoeksData { get; set; } // Format van vragen etc in JSON format
}