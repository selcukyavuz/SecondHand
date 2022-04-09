using System.Text.Json.Serialization;

namespace SecondHand.Web.Models
{
    public class Token : SecondHand.Library.Models.Strava.TokenExchange
    {
        [JsonPropertyName("athlete")]
        public SecondHand.Library.Models.Strava.Athlete? Athlete { get; set; }
    }
}