namespace SecondHand.Models.Advertisement;

using System.ComponentModel.DataAnnotations;

public partial class Product
{
    [Key]
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string? Name { get; set; }
}