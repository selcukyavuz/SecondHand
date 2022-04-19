namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Adversitement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class MarkAdapter : BaseAdapter
{
    public MarkAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Mark> Get()
    {
        var path = "/mark";        
        return RestClient.Get<List<Mark>>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }

    public Mark Get(int id)
    {
        var path = "/mark/" + id;
        return RestClient.Get<Mark>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }
}