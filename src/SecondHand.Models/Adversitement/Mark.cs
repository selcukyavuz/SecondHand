using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Adversitement;

public class Mark
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }
}