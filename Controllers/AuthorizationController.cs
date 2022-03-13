using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using StravaAuth.Common;
using StravaAuth.Response;
using StravaAuth.Settings;

namespace StravaAuth.Controllers;

public class AuthorizationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StravaSettings _staravaSettings;
    private readonly IHttpContextAccessor _accessor;
    public StravaSettings? staravaSettings { get; private set; }

     public AuthorizationController(ILogger<HomeController> logger,IOptions<StravaSettings> options,IHttpContextAccessor accessor)
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
        TokenExchange? tokenExchangeResponse = JsonSerializer.Deserialize<TokenExchange>(response.Content!,StravaAuthJsonSerializerSettings.Settings);
        _accessor.HttpContext?.Session.SetString("access_token",tokenExchangeResponse?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);
        return Redirect("~/");
    }
}