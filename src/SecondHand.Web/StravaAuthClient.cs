namespace SecondHand.Web;

using SecondHand.Web.Adapter;
using SecondHand.Web.Request.Common;

public class SecondHandWebClient
{
   private const string BaseUrl = "https://www.strava.com/api/v3";
   private readonly AthleteAdapter _atheleteAdapter;

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

      _atheleteAdapter = new AthleteAdapter(requestOptions);
   }

   public AthleteAdapter Athlete()
   {
      return _atheleteAdapter;
   }
}
