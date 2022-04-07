using System.ComponentModel.DataAnnotations;
namespace SecondHand.Library.Models;

public class PersonModel
{
    [Key]
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
} 