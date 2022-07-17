namespace SecondHand.Models.Advertisement;

using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public long Id { get; set; }

    public string? Name { get; set; }
}