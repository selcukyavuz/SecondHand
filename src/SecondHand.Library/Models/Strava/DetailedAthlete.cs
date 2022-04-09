namespace SecondHand.Library.Models.Strava;

using System.Text.Json.Serialization;

public class DetailedAthlete : SummaryAthlete
{
    [JsonPropertyName("follower_count")]
    public int FollowerCount { get; set; }

    [JsonPropertyName("friend_count")]
    public int FriendCount { get; set; }

    [JsonPropertyName("measurement_preference")]
    public string? MeasurementPreference { get; set; }

    [JsonPropertyName("ftp")]
    public int? Ftp { get; set; }

    [JsonPropertyName("weight")]
    public float Weight { get; set; }

    [JsonPropertyName("clubs")]
    public List<Club>? Clubs { get; set; }

    [JsonPropertyName("bikes")]
    public IList<Bike>? Bikes { get; set; }

    [JsonPropertyName("shoes")]
    public IList<Shoe>? Shoes { get; set; }    
}