using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SecondHand.Library.Models;

public class TokenExchange
{
    [Key]
    public Guid Id { get; set; }
    public string? DetailedAthlete { get; set; }
    
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}
