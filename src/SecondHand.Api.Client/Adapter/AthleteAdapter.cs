namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Strava;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class AthleteAdapter : BaseAdapter
{
    public AthleteAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public Athlete Get(int id)
    {
        var path = "/athlete?id=" + id;
        return RestClient.Get<Athlete>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!);
    }

    public Athlete Create(Athlete athlete)
    {
        const string path = "/athlete";
        return RestClient.Post<Athlete>(RequestOptions.BaseUrl + path, CreateHeaders(RequestOptions)!, athlete);
    }
}