namespace SecondHand.Models.Strava;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonPropertyName("detailed_athlete_id")]
    public long AthleteId { get; set; }
}
