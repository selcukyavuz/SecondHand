namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class MarkAdapter : BaseAdapter
{
    public MarkAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Mark> Get()
    {
        const string path = "/mark";
        return RestClient.Get<List<Mark>>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }

    public Mark Get(int id)
    {
        string path = "/mark/" + id;
        return RestClient.Get<Mark>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }
}