using System.ComponentModel.DataAnnotations;

namespace api;

public class OnderzoeksType
{
    [Key]
    public int BitFlag { get; set; }
    public string Type { get; set; }
}