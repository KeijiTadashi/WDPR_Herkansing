using System.ComponentModel.DataAnnotations;

namespace api;

public class Beperking
{
    public int Id { get; set; }
    public int BitFlag { get; set; }
    public string Naam { get; set; }
}