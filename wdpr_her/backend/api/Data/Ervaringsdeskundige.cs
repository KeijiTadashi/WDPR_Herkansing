using System.ComponentModel;

namespace api;

public class Ervaringsdeskundige : Persoon
{
    public string Postcode { get; set; }
    public List<Aandoening>? Aandoending { get; set; }
    public List<Hulpmiddel>? Hulpmiddel { get; set; }
    public List<Beperking>? Beperking { get; set; }
    public Benadering? VoorkeurBenadering { get; set; }
    public List<OnderzoeksType>? VoorkeurOnderzoek { get; set; }

    [DefaultValue(true)] public bool MagBenaderdWorden { get; set; }

    //Todo Beschikbaarheid op een bepaalde manier
    public Verzorger? Verzorger { get; set; }
}