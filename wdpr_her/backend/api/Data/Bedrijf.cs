namespace api;

public class Bedrijf : Gebruiker
{
    public string Naam { get; set; }
    public int Kvk { get; set; }
    public string? Locatie { get; set; }
    public string? Website { get; set; } // In ERR Link
}