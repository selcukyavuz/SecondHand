namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class ProductAdapter : BaseAdapter
{
    public ProductAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Product> Get() => RestClient.Get<List<Product>>($"{RequestOptions.BaseUrl}/product", CreateHeaders(RequestOptions)!);

    public Product Get(int id) =>  RestClient.Get<Product>($"{RequestOptions.BaseUrl}/product/{id}", CreateHeaders(RequestOptions)!);
}