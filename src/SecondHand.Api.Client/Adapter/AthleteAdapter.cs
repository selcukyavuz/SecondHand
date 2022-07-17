namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Strava;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class AthleteAdapter : BaseAdapter
{
    public AthleteAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public Athlete Get(int id) => RestClient.Get<Athlete>($"{RequestOptions.BaseUrl}/athlete?id={id}", CreateHeaders(RequestOptions)!);

    public Athlete Create(Athlete athlete)=> RestClient.Post<Athlete>($"{RequestOptions.BaseUrl}/athlete", CreateHeaders(RequestOptions)!, athlete);
}