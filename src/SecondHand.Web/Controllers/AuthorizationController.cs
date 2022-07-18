using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;
using SecondHand.Web.Models;

namespace SecondHand.Web.Controllers;

public class AuthorizationController : BaseController
{
    private readonly StravaSettings _stravaSettings;
    private readonly IHttpContextAccessor _accessor;

    public AuthorizationController(IOptions<StravaSettings> options,IHttpContextAccessor accessor,IConfiguration configuration) : base(configuration)
    {
        _stravaSettings = options.Value;
        _accessor = accessor;
    }

    [HttpGet("~/exchange_token")]
    public async Task<IActionResult> ExchangeToken(string code,string scope)
    {
        StravaHelper stravaHelper = new(_accessor);
        Token token = await stravaHelper.GetToken(_stravaSettings, code);
        var athlete = SecondHandApiClient.Athlete().Create(token?.Athlete!);

        _accessor.HttpContext?.Session.SetString("AthleteId",token?.AthleteId!.ToString()!);
        _accessor.HttpContext?.Session.SetString("access_token",token?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        token!.AthleteId = athlete.Id;
        _ = SecondHandApiClient.TokenExchange().Create(token);

        return Redirect("~/");
    }
}