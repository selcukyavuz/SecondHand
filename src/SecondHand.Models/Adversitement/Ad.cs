using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Adversitement;

public class Ad
{
    [Key]
    public long Id { get; set; }
    public Category? Category { get; set; }
    public Product? Product { get; set; }
    public Mark? Mark { get; set; }
    public int ModelYear { get; set; }
    public string? Subject { get; set; }
    public float Price { get; set; }
    public string? PriceCurrency { get; set; }
    public DateTime PublishDate { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

}