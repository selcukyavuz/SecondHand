using System.Text.Json.Serialization;

namespace SecondHand.Web.Models
{
    public class Token : SecondHand.Models.Strava.TokenExchange
    {
        [JsonPropertyName("athlete")]
        public SecondHand.Models.Strava.Athlete? Athlete { get; set; }
    }
}