namespace SecondHand.Models.Strava;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Athlete
{
    [Key]
    [JsonPropertyName("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }

    [JsonPropertyName("resource_state")]
    public int? ResourceState { get; set; }

    [JsonPropertyName("firstname")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastname")]
    public string? LastName { get; set; }

    [JsonPropertyName("profile_medium")]
    public string? ProfileMedium { get; set; }

    [JsonPropertyName("profile")]
    public string? Profile { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("sex")]
    public string? Sex { get; set; }

    [JsonPropertyName("premium")]
    public bool Premium { get; set; }

    [JsonPropertyName("summit")]
    public bool Summit { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt{ get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt {get; set;}

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