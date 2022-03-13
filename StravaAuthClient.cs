namespace StravaAuth;

using StravaAuth.Adapter;
using StravaAuth.Request.Common;

public class StravaAuthClient
{
   private const string BaseUrl = "https://www.strava.com/api/v3";
   private readonly AthleteAdapter _atheleteAdapter;

   public StravaAuthClient(string apiKey, string secretKey) : this(apiKey, secretKey, BaseUrl)
   {
   }

   public StravaAuthClient(string apiKey, string secretKey, string baseUrl)
   {
      var requestOptions = new RequestOptions
      {
            ApiKey = apiKey,
            SecretKey = secretKey,
            BaseUrl = baseUrl
      };

      _atheleteAdapter = new AthleteAdapter(requestOptions);
   }

   public AthleteAdapter Athlete()
   {
      return _atheleteAdapter;
   }
}
