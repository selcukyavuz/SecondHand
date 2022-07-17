namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class CategoryAdapter : BaseAdapter
{
    public CategoryAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Category> Get() =>  RestClient.Get<List<Category>>($"{RequestOptions.BaseUrl}/category", CreateHeaders(RequestOptions)!);

    public Category Get(int id) => RestClient.Get<Category>($"{RequestOptions.BaseUrl}/category/{id}", CreateHeaders(RequestOptions)!);
}