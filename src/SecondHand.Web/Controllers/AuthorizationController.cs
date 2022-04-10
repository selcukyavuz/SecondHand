using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using SecondHand.Library.Models.Strava;
using SecondHand.Api.Client.Settings;
using SecondHand.Web.Models;

namespace SecondHand.Web.Controllers;

public class AuthorizationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StravaSettings _staravaSettings;
    private readonly IHttpContextAccessor _accessor;
    public StravaSettings? staravaSettings { get; private set; }
    private readonly IConfiguration _configuration;
     public AuthorizationController(
        ILogger<HomeController> logger,
        IOptions<StravaSettings> options,
        IHttpContextAccessor accessor,
        IConfiguration configuration)
    {
        _logger = logger;
        _staravaSettings = options.Value;
        _accessor = accessor;
        _configuration = configuration;
    }

    [HttpGet("~/exchange_token")]
    public async Task<IActionResult> ExchangeToken(string code,string scope)
    {
        var SecondHandApiUrl = _configuration["SecondHandApiUrl"];
        var client = new RestClient(_staravaSettings.TokenExchangeUrl);
        var request = new RestRequest();
        request.RequestFormat = DataFormat.Json;
        request.AddParameter("client_secret", _staravaSettings.ClientSecret);
        request.AddParameter("client_id", _staravaSettings.ClientId);
        request.AddParameter("code", code);
        request.AddParameter("grant_type", "authorization_code");
        request.AddParameter("redirect_uri", "https://localhost:7293/exchange_token&approval_prompt=force&scope=profile:read_all");
        RestResponse response = await client.ExecutePostAsync(request);
        Token? token = JsonSerializer.Deserialize<Token>(
            response.Content!,
            SecondHand.Api.Client.Common.JsonSerializerSettings.Settings);
        _accessor.HttpContext?.Session.SetString("access_token",token?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        var athleteClient = new RestClient(SecondHandApiUrl + "/api/Athlete");
        var athleteRequest = new RestRequest();
        athleteRequest.AddHeader("accept", "text/plain");
        athleteRequest.AddHeader("Content-Type", "application/json");
        athleteRequest.AddBody(token?.Athlete!);
        RestResponse athleteResponse = await athleteClient.ExecutePostAsync(athleteRequest);
        Athlete? athlete = JsonSerializer.Deserialize<Athlete>(
            athleteResponse.Content!,
            SecondHand.Api.Client.Common.JsonSerializerSettings.Settings);

        token.AthleteId = athlete.Id;

        var tokenExchangeClient = new RestClient(SecondHandApiUrl + "/api/TokenExchange");
        var tokenExchangeRequest = new RestRequest();
        tokenExchangeRequest.AddHeader("accept", "text/plain");
        tokenExchangeRequest.AddHeader("Content-Type", "application/json");
        tokenExchangeRequest.AddBody(token!);
        RestResponse restResponseTokenExchange = await tokenExchangeClient.ExecutePostAsync(tokenExchangeRequest);
        TokenExchange? responseTokenExchange = JsonSerializer.Deserialize<TokenExchange>(
            restResponseTokenExchange.Content!,
            SecondHand.Api.Client.Common.JsonSerializerSettings.Settings);

        return Redirect("~/");
    }
}