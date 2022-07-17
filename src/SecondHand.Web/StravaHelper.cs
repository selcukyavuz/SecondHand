using System.Text.Json;
using RestSharp;
using SecondHand.Api.Client.Settings;
using SecondHand.Web.Models;

namespace SecondHand.Web;

public class StravaHelper
{
    private readonly IHttpContextAccessor _accessor;

    public StravaHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public async Task<Token> GetToken(StravaSettings stravaSettings, string code)
    {
        var baseUrl = $"{_accessor?.HttpContext?.Request.Scheme}://{_accessor?.HttpContext?.Request.Host}{_accessor?.HttpContext?.Request.PathBase}";
        var client = new RestClient(stravaSettings.TokenExchangeUrl);
        var request = new RestRequest
        {
            RequestFormat = DataFormat.Json
        };
        request.AddParameter("client_secret", stravaSettings.ClientSecret);
        request.AddParameter("client_id", stravaSettings.ClientId);
        request.AddParameter("code", code);
        request.AddParameter("grant_type", "authorization_code");
        request.AddParameter("redirect_uri", baseUrl + "/exchange_token&approval_prompt=force&scope=profile:read_all");
        RestResponse response = await client.ExecutePostAsync(request);
        return JsonSerializer.Deserialize<Token>(response.Content!,Api.Client.Common.JsonSerializerSettings.Settings)!;
    }
}