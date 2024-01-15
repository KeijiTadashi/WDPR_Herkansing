using System.ComponentModel.DataAnnotations;

namespace api;

public class OnderzoeksType
{
    public int Id { get; set; }
    public string Type { get; set; }
    public List<Onderzoek>? Onderzoeken { get; set; }
    public List<Ervaringsdeskundige>? Ervaringsdeskundigen { get; set; }
}