namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class CategoryAdapter : BaseAdapter
{
    public CategoryAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Category> Get()
    {
        const string path = "/category";
        return RestClient.Get<List<Category>>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }

    public Category Get(int id)
    {
        string path = "/category/" + id;
        return RestClient.Get<Category>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }
}