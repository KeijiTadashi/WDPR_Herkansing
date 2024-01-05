public class Onderzoek
{
    private string titel { get; set; }
    private string onderzoek_ID { get; set; }
    private User uitvoerder { get; set; }
    private string locatie { get; set; }
    private string beloning { get; set; }
    private Onderzoek_Type type_Onderzoek { get; set; }
    private bool status_Gereageerd { get; set; }
    private string beschrijving { get; set; }
    private string onderzoek_Data { get; set; }
    private OnderzoekMethode(Type type)
    {
    }
}