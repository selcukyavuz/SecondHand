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
        string path = "/Ad?id=" + id;
        return RestClient.Get<Ad>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }

    public Ad Create(Ad Ad)
    {
        const string path = "/Ad";
        return RestClient.Post<Ad>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!, Ad);
    }
}