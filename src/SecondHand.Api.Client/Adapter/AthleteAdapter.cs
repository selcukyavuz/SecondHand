namespace SecondHand.Api.Client.Adapter;

using SecondHand.Library.Models.Strava;
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
}