using System.ComponentModel.DataAnnotations;

namespace api;

public class OpdrachtRespons
{
    [Key]
    public int ResponsId { get; set; }
    // public int UserId { get; set; }
    // public int OnderzoekId { get; set; }
    public string VraagMetAntwoordenJSON { get; set; }

    public Gebruiker Gebruiker { get; set; }
    public Onderzoek Onderzoek { get; set; }
}