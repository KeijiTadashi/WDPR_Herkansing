using System.ComponentModel.DataAnnotations;

namespace api;

public class Benadering
{
    [Key]
    public int BitFlag { get; set; }
    public string Soort { get; set; }
}