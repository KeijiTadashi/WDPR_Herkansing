using System.ComponentModel.DataAnnotations;

namespace api;

public class Hulpmiddel
{
    [Key]
    public int BitFlag { get; set; }
    public string Naam { get; set; }
}