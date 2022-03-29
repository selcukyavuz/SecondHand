namespace SecondHandGear.Web;

using SecondHandGear.Web.Adapter;
using SecondHandGear.Web.Request.Common;

public class SecondHandGearWebClient
{
   private const string BaseUrl = "https://www.strava.com/api/v3";
   private readonly AthleteAdapter _atheleteAdapter;

   public SecondHandGearWebClient(string apiKey, string secretKey) : this(apiKey, secretKey, BaseUrl)
   {
   }

   public SecondHandGearWebClient(string apiKey, string secretKey, string baseUrl)
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
