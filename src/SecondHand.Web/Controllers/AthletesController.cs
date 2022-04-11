using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;

namespace SecondHand.Web.Controllers;

public class AthletesController : Controller
{
    private readonly ILogger<AthletesController> _logger;
    private readonly IConfiguration _configuration;
    private readonly SecondHandWebClient _SecondHandWebClient;
    private readonly SecondHandApiClient _secondHandApiClient;
    private readonly IHttpContextAccessor _accessor;

    public AthletesController(ILogger<AthletesController> logger,IConfiguration configuration,IHttpContextAccessor accessor)
    {
        _logger = logger;
        _configuration = configuration;
        _accessor = accessor;
        _SecondHandWebClient = new SecondHandWebClient(string.Empty, string.Empty, configuration["Strava:ApiUrl"]!);
        _secondHandApiClient = new SecondHandApiClient(
            string.Empty, 
            string.Empty, 
            configuration["SecondHandApiUrl"]!);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("~/athlete/stats")]
    public async Task<IActionResult> GetStats()
    {
        var access_token = _accessor.HttpContext?.Session.GetString("access_token");
        var athlete = await Task.Run(() => _secondHandApiClient.Athlete().GetStats(access_token!));
        return View(athlete);
    }
}