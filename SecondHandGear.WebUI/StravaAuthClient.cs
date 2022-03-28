namespace StravaStore;

using StravaStore.Adapter;
using StravaStore.Request.Common;

public class StravaStoreClient
{
   private const string BaseUrl = "https://www.strava.com/api/v3";
   private readonly AthleteAdapter _atheleteAdapter;

   public StravaStoreClient(string apiKey, string secretKey) : this(apiKey, secretKey, BaseUrl)
   {
   }

   public StravaStoreClient(string apiKey, string secretKey, string baseUrl)
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
