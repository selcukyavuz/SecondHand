namespace SecondHand.Api.Client.Adapter;

using SecondHand.Models.Strava;
using SecondHand.Api.Client.Net;
using SecondHand.Api.Client.Request.Common;

public class AthleteAdapter : BaseAdapter
{
    public AthleteAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public Athlete GetStats(string access_token)
    {
        var path = "/athlete?access_token=" + access_token;        
        return RestClient.Get<Athlete>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }

    public Athlete Create(Athlete athlete)
    {
        var path = "/athlete";
        return RestClient.Post<Athlete>(RequestOptions.BaseUrl + path, CreateHeaders(athlete,path,RequestOptions)!, athlete);
    }
}