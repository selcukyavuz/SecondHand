using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using SecondHand.Library.Models.Strava;
using SecondHand.Web.Common;
using SecondHand.Web.Settings;
using SecondHand.Web.Models;

namespace SecondHand.Web.Controllers;

public class AuthorizationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StravaSettings _staravaSettings;
    private readonly IHttpContextAccessor _accessor;
    public StravaSettings? staravaSettings { get; private set; }
     public AuthorizationController(
        ILogger<HomeController> logger,
        IOptions<StravaSettings> options,
        IHttpContextAccessor accessor       
        )
    {
        _logger = logger;
        _staravaSettings = options.Value;
        _accessor = accessor;
    }

    [HttpGet("~/exchange_token")]
    public async Task<IActionResult> ExchangeToken(string code,string scope)
    {
        var client = new RestClient(_staravaSettings.TokenExchangeUrl);
        var request = new RestRequest();
        request.RequestFormat = DataFormat.Json;
        request.AddParameter("client_secret", _staravaSettings.ClientSecret);
        request.AddParameter("client_id", _staravaSettings.ClientId);
        request.AddParameter("code", code);
        request.AddParameter("grant_type", "authorization_code");
        request.AddParameter("redirect_uri", "https://localhost:7293/exchange_token&approval_prompt=force&scope=profile:read_all");
        RestResponse response = await client.ExecutePostAsync(request);
        Token? token = JsonSerializer.Deserialize<Token>(response.Content!,SecondHandWebJsonSerializerSettings.Settings);
        _accessor.HttpContext?.Session.SetString("access_token",token?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        var athleteClient = new RestClient("https://localhost:7269/api/DetailedAthlete");
        var athleteRequest = new RestRequest();
        athleteRequest.AddHeader("accept", "text/plain");
        athleteRequest.AddHeader("Content-Type", "application/json");
        athleteRequest.AddBody(token?.DetailedAthlete!);
        RestResponse athleteResponse = await athleteClient.ExecutePostAsync(athleteRequest);
        DetailedAthlete? detailedAthlete = JsonSerializer.Deserialize<DetailedAthlete>(athleteResponse.Content!,SecondHandWebJsonSerializerSettings.Settings);

        token.DetailedAthleteId = detailedAthlete.Id;

        var tokenExchangeClient = new RestClient("https://localhost:7269/api/TokenExchange");
        var tokenExchangeRequest = new RestRequest();
        tokenExchangeRequest.AddHeader("accept", "text/plain");
        tokenExchangeRequest.AddHeader("Content-Type", "application/json");
        tokenExchangeRequest.AddBody(token!);
        RestResponse restResponseTokenExchange = await tokenExchangeClient.ExecutePostAsync(tokenExchangeRequest);
        TokenExchange? responseTokenExchange = JsonSerializer.Deserialize<TokenExchange>(restResponseTokenExchange.Content!,SecondHandWebJsonSerializerSettings.Settings);

        return Redirect("~/");
    }
}