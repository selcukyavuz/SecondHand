namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Adversitement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class CategoryAdapter : BaseAdapter
{
    public CategoryAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Category> Get()
    {
        var path = "/category";        
        return RestClient.Get<List<Category>>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }

    public Category Get(int id)
    {
        var path = "/category/" + id;
        return RestClient.Get<Category>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }
}