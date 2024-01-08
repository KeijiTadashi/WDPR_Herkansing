using System.ComponentModel.DataAnnotations;

namespace api;

public class Aandoening
{
    [Key]
    public int BitFlag { get; set; }
    public string Naam { get; set; }
}