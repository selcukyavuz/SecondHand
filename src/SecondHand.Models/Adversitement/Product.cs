using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Adversitement;
public partial class Product
{
    [Key]
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string? Name { get; set; }
}
