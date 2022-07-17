namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Strava;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class TokenExchangeAdapter : BaseAdapter
{
    public TokenExchangeAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }
    public TokenExchange Create(TokenExchange TokenExchange)
        => RestClient.Post<TokenExchange>($"{RequestOptions.BaseUrl}/TokenExchange", CreateHeaders(RequestOptions)!, TokenExchange);
}