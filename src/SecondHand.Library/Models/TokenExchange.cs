using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SecondHand.Library.Models.Strava;

namespace SecondHand.Library.Models;

public class TokenExchange
{
    [Key]
    [JsonIgnore]
    public long? Id { get; set; }
    
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}
