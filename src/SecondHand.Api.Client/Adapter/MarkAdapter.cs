namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Advertisement;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class MarkAdapter : BaseAdapter
{
    public MarkAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public List<Mark> Get() => RestClient.Get<List<Mark>>($"{RequestOptions.BaseUrl}/mark", CreateHeaders(RequestOptions)!);

    public Mark Get(int id) => RestClient.Get<Mark>($"{RequestOptions.BaseUrl}/mark/{id}", CreateHeaders(RequestOptions)!);
}