namespace SecondHand.Api.Client.Adapter;

using SecondHand.Library.Models.Strava;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class TokenExchangeAdapter : BaseAdapter
{
    public TokenExchangeAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }
    public TokenExchange Create(TokenExchange TokenExchange) => RestClient.Post<TokenExchange>(RequestOptions.BaseUrl + "/TokenExchange", CreateHeaders(TokenExchange, "/TokenExchange", RequestOptions)!, TokenExchange);
}