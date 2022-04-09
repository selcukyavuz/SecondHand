namespace SecondHand.Web.Adapter;

using SecondHand.Library.Models.Strava;
using SecondHand.Web.Net;
using SecondHand.Web.Request.Common;
using SecondHand.Web.Response;

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