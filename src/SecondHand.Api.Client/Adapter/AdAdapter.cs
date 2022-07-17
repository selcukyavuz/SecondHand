namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class AdAdapter : BaseAdapter
{
    public AdAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public Ad Get(int id)
    {
        return RestClient.Get<Ad>($"{RequestOptions.BaseUrl}/Ad?id=" + id, CreateHeaders(RequestOptions)!);
    }

    public Ad Create(Ad Ad)
    {
        return RestClient.Post<Ad>($"{RequestOptions.BaseUrl}/Ad", CreateHeaders(RequestOptions)!, Ad);
    }
}