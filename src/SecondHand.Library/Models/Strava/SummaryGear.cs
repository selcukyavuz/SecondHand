namespace SecondHand.Library.Models.Strava;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class SummaryGear
{
    [Key]
    [JsonIgnore]
    public int SummaryGearId { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("resource_state")]
    public int ResourceState { get; set; }

    [JsonPropertyName("primary")]
    public bool Primary { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("distance")]
    public float Distance { get; set; }
}