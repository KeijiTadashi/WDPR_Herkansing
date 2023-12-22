using System.ComponentModel;

namespace api;

public class Ervaringsdeskundige : Gebruiker
{
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string Postcode { get; set; }
    public int? Aandoending { get; set; }
    public int? Hulpmiddel { get; set; }
    public int? Beperking { get; set; }
    public int? VoorkeurBenadering { get; set; }
    public int? VooerkeurOnderzoek { get; set; }
    [DefaultValue(true)]
    public bool MagBenaderdWorden { get; set; }
    //Todo Beschikbaarheid op een bepaalde manier
    public Verzorger? Verzorger { get; set; }

}