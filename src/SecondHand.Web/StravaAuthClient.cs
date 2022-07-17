namespace SecondHand.Web;

using SecondHand.Api.Client.Adapter;
using SecondHand.Api.Client.Request.Common;

public class SecondHandWebClient
{
   private const string BaseUrl = "https://www.strava.com/api/v3";
   private readonly AthleteAdapter _athleteAdapter;

   public SecondHandWebClient(string apiKey, string secretKey) : this(apiKey, secretKey, BaseUrl)
   {
   }

   public SecondHandWebClient(string apiKey, string secretKey, string baseUrl)
   {
      var requestOptions = new RequestOptions
      {
            ApiKey = apiKey,
            SecretKey = secretKey,
            BaseUrl = baseUrl
      };

      _athleteAdapter = new AthleteAdapter(requestOptions);
   }

   public AthleteAdapter Athlete()
   {
      return _athleteAdapter;
   }
}
