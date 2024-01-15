using System.ComponentModel.DataAnnotations;

namespace api;

public class OpdrachtRespons
{
    public int ResponsID { get; set; }
    public int UserID { get; set; }
    public int OnderzoekID { get; set; }
    public string VraagMetAntwoordenJSON { get; set; }

    public Gebruiker Gebruiker { get; set; }
    public Onderzoek Onderzoek { get; set; }
}