public class Ervaringdeskundige
{
    private string voornaam { get; set; }
    private string achternaam { get; set; }
    private string postcode { get; set; }
    private string telefoon_Nr { get; set; }

    private List<Beperking> beperking = new List<Beperking>();
    private List<Aandoening> aandoening = new List<Aandoening>();
    private Hulpmiddelel[]
    private EBenadering voor_benadering { get; set; }
    private List<Onderzoek_Type> voorkeur_Onderzoek = new List<Onderzoek_Type>();
    private bool mag_Benaderd_Worden = true {get; set;}
    private DateTimePicker beschikbaarheid { get; set; }
    private Verzorger verzorger? {get; set;}
    
}