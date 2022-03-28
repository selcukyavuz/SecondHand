namespace StravaStore.Response;

using System.Text.Json.Serialization;
using StravaStore.Model;

public class SummaryGear
{
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