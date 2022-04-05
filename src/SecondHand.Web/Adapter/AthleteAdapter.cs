namespace SecondHand.Web.Adapter;

using SecondHand.Web.Net;
using SecondHand.Web.Request.Common;
using SecondHand.Web.Response;

public class AthleteAdapter : BaseAdapter
{
    public AthleteAdapter(RequestOptions requestOptions) : base(requestOptions)
    {
    }

    public DetailedAthlete GetStats(string access_token)
    {
        var path = "/athlete?access_token=" + access_token;        
        return RestClient.Get<DetailedAthlete>(RequestOptions.BaseUrl + path,CreateHeaders(path, RequestOptions)!);  
    }
}