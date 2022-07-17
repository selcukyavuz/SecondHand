namespace SecondHand.Models;

using System.ComponentModel.DataAnnotations;

public class OAuthMembership
{
    [Key]
    public string? Provider { get; set; }

    [Key]
    public string? ProviderUserId { get; set; }

    public int UserId { get; set; }
}