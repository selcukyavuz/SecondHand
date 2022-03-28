namespace StravaStore.Adapter;

using StravaStore.Net;
using StravaStore.Request.Common;
using StravaStore.Response;

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