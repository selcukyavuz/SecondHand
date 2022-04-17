namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Adversitement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class ProductAdapter : BaseAdapter
{
    public ProductAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Product> Get()
    {
        var path = "/product";        
        return RestClient.Get<List<Product>>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }

    public Product Get(int id)
    {
        var path = "/product/" + id;
        return RestClient.Get<Product>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }
}