
using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Adversitement;

public class Category
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }
}