namespace SecondHand.Api.Client;

using SecondHand.Api.Client.Adapter;
using SecondHand.Api.Client.Request.Common;

public class SecondHandApiClient : ISecondHandApiClient
{
   private const string BaseUrl = "https://localhost:7269";
   private readonly AthleteAdapter _athleteAdapter;
   private readonly TokenExchangeAdapter _tokenExchangeAdapter;
   public SecondHandApiClient(string apiKey, string secretKey) : this(apiKey, secretKey, BaseUrl)
   {
   }

   public SecondHandApiClient(string apiKey, string secretKey, string baseUrl)
   {
      var requestOptions = new RequestOptions
      {
            ApiKey = apiKey,
            SecretKey = secretKey,
            BaseUrl = baseUrl
      };

      _athleteAdapter = new AthleteAdapter(requestOptions);
      _tokenExchangeAdapter = new TokenExchangeAdapter(requestOptions);
   }

   public AthleteAdapter Athlete() => _athleteAdapter;
    public TokenExchangeAdapter TokenExchange() => _tokenExchangeAdapter;
}