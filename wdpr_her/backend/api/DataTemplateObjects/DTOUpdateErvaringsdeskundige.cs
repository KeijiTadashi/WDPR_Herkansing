namespace api.DataTemplate;

public class DTOUpdateErvaringsdeskundige
{
    public string? UserName { get; set; } //Only needed if you aren't the user (aka beheerder)
    public string? NewUserName { get; set; }
    public string? Voornaam { get; set; }
    public string? Achternaam { get; set; }
    public string? Postcode { get; set; }
    public string? NieuwWachtwoord { get; set; }
    public string? HuidigWachtwoord { get; set; }
    public string? TelefoonNummer { get; set; }
    public string? Email { get; set; }
}