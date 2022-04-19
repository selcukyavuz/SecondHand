namespace SecondHand.Api.Client;

using SecondHand.Api.Client.Adapter;
using SecondHand.Api.Client.Request.Common;

public class SecondHandApiClient : ISecondHandApiClient
{
   
   private readonly AdAdapter _adAdapter;
   private readonly CategoryAdapter _categoryAdapter;
   private readonly ProductAdapter _productAdapter;
   private readonly MarkAdapter _markAdapter;
   private readonly AthleteAdapter _athleteAdapter;
   private readonly TokenExchangeAdapter _tokenExchangeAdapter;

   public SecondHandApiClient(string apiKey, string secretKey, string baseUrl)
   {
      var requestOptions = new RequestOptions
      {
            ApiKey = apiKey,
            SecretKey = secretKey,
            BaseUrl = baseUrl
      };

      _adAdapter = new AdAdapter(requestOptions);
      _categoryAdapter = new CategoryAdapter(requestOptions);
      _productAdapter = new ProductAdapter(requestOptions);
      _markAdapter = new MarkAdapter(requestOptions);
      _athleteAdapter = new AthleteAdapter(requestOptions);
      _tokenExchangeAdapter = new TokenExchangeAdapter(requestOptions);
   }

   public AdAdapter Ad() => _adAdapter;
   public CategoryAdapter Category() => _categoryAdapter;
   public ProductAdapter Product() => _productAdapter;
   public MarkAdapter Mark() => _markAdapter;
   public AthleteAdapter Athlete() => _athleteAdapter;
   public TokenExchangeAdapter TokenExchange() => _tokenExchangeAdapter;
}