using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Advertisement;

public class Mark
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }
}