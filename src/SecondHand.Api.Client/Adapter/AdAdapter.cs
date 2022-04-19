namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Adversitement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class AdAdapter : BaseAdapter
{
    public AdAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public Ad Get(int id)
    {
        var path = "/Ad?id=" + id;        
        return RestClient.Get<Ad>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }

    public Ad Create(Ad Ad)
    {
        var path = "/Ad";
        return RestClient.Post<Ad>(RequestOptions.BaseUrl + path, CreateHeaders(Ad,path,RequestOptions)!, Ad);
    }
}