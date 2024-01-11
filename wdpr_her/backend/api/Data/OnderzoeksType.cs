using System.ComponentModel.DataAnnotations;

namespace api;

public class OnderzoeksType
{
    public int Id { get; set; }
    public int BitFlag { get; set; }
    public string Type { get; set; }
}