using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecondHand.Api.Client;
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
    private readonly SecondHandApiClient _secondHandApiClient;

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
        _secondHandApiClient = new SecondHandApiClient(
            string.Empty, 
            string.Empty, 
            configuration["SecondHandApiUrl"]!);

    }

    [HttpGet("~/exchange_token")]
    public async Task<IActionResult> ExchangeToken(string code,string scope)
    {
        var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        StravaHelper stravaHelper = new StravaHelper(_accessor);
        Token token = await stravaHelper.GetToken(_staravaSettings, code);
        var athlete = _secondHandApiClient.Athlete().Create(token?.Athlete!);

        _accessor.HttpContext?.Session.SetString("access_token",token?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        token!.AthleteId = athlete.Id;
        var tokenExchange = _secondHandApiClient.TokenExchange().Create(token);

        return Redirect("~/");
    }
}