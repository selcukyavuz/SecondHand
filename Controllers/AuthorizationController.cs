using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using StravaStore.Common;
using StravaStore.Data;
using StravaStore.Models;
using StravaStore.Response;
using StravaStore.Settings;

namespace StravaStore.Controllers;

public class AuthorizationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StravaSettings _staravaSettings;
    private readonly IHttpContextAccessor _accessor;
    public StravaSettings? staravaSettings { get; private set; }
    private readonly StravaStoreContext _context;

     public AuthorizationController(
        ILogger<HomeController> logger,
        IOptions<StravaSettings> options,
        IHttpContextAccessor accessor,
        StravaStoreContext context
        )
    {
        _logger = logger;
        _staravaSettings = options.Value;
        _accessor = accessor;
        _context = context;
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
        TokenExchange? tokenExchangeResponse = JsonSerializer.Deserialize<TokenExchange>(response.Content!,StravaStoreJsonSerializerSettings.Settings);
        _accessor.HttpContext?.Session.SetString("access_token",tokenExchangeResponse?.AccessToken!);
        _accessor.HttpContext?.Session.SetString("scope",scope);

        _context.Database.EnsureCreated();

        _context.Add<TokenPool>(new TokenPool
        {
            AccessToken = tokenExchangeResponse?.AccessToken,
            SessionID = _accessor.HttpContext?.Session.Id
        });

        _context.SaveChanges();

        return Redirect("~/");
    }
}