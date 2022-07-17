namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class ProductAdapter : BaseAdapter
{
    public ProductAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Product> Get()
    {
        const string path = "/product";
        return RestClient.Get<List<Product>>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }

    public Product Get(int id)
    {
        string path = "/product/" + id;
        return RestClient.Get<Product>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }
}