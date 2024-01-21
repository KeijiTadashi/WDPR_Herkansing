namespace api.DataTemplate;

public class DTOGetErvaringsdeskundige
{
    public string? UserName { get; set; } //Only needed if you aren't the user (aka beheerder)
    public string? Voornaam { get; set; }
    public string? Achternaam { get; set; }
    public string? Postcode { get; set; }
    public string? TelefoonNummer { get; set; }
    public string? Email { get; set; }
}