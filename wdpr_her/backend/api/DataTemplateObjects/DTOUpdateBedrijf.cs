namespace api.DataTemplate;

public class DTOUpdateBedrijf
{
    public string? UserName { get; set; } //Only needed if you aren't the user (aka beheerder)
    public string? NewUserName { get; set; }
    public string? Naam { get; set; }
    public string? Postcode { get; set; }
    public string? NieuwWachtwoord { get; set; }
    public string? HuidigWachtwoord { get; set; }
    public string? TelefoonNummer { get; set; }
    public string? Email { get; set; }
    public long? Kvk { get; set; }
    public string? Website { get; set; }
    public string? Locatie { get; set; }
}