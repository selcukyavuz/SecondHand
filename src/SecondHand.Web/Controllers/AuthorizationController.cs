using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecondHand.Api.Client;
using SecondHand.Api.Client.Settings;
using SecondHand.Web.Models;

namespace SecondHand.Web.Controllers;

public class AuthorizationController : Controller
{
    private readonly StravaSettings _stravaSettings;
    private readonly IHttpContextAccessor _accessor;
    private readonly SecondHandApiClient _secondHandApiClient;

    public AuthorizationController(IOptions<StravaSettings> options,IHttpContextAccessor accessor,IConfiguration configuration)
    {
        _stravaSettings = options.Value;
        _accessor = accessor;
        _secondHandApiClient = new SecondHandApiClient(string.Empty,string.Empty,configuration["SecondHandApiUrl"]!);
    }

    [HttpGet("~/exchange_token")]
    public async Task<IActionResult> ExchangeToken(string code,string scope)
    {
        StravaHelper stravaHelper = new(_accessor);
        Token token = await stravaHelper.GetToken(_stravaSettings, code);
        var athlete = _secondHandApiClient.Athlete().Create(token?.Athlete!);

        _accessor.HttpContext?.Session.SetString("AthleteId",token?.AthleteId!.ToString()!);
        _accessor.HttpContext?.Session.SetString("access_token",token?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        token!.AthleteId = athlete.Id;
        _ = _secondHandApiClient.TokenExchange().Create(token);

        return Redirect("~/");
    }
}