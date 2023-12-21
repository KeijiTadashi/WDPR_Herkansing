namespace api;

public class Verzorger
{
    public int Id { get; init; }
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string? Email { get; set; }
    public string? Telefoon { get; set; }
}