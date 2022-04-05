using Microsoft.AspNetCore.Mvc;
using SecondHand.Web.Data;

namespace SecondHand.Web.Controllers;

public class AthletesController : Controller
{
    private readonly SecondHandWebClient _SecondHandWebClient = new SecondHandWebClient(
        "", 
        "", 
        "https://www.strava.com/api/v3");

    private readonly ILogger<AthletesController> _logger;
    private readonly SecondHandWebContext _context;

    public AthletesController(
        ILogger<AthletesController> logger,
        SecondHandWebContext context
        )
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("~/athlete/stats")]
    public async Task<IActionResult> GetStats()
    {
        #pragma warning disable CS8602
        var access_token = _context.TokenPools?.FirstOrDefault(c=>c.SessionID == HttpContext.Session.Id).AccessToken;
        #pragma warning restore CS8602
        return View(await Task.Run(()=> _SecondHandWebClient.Athlete().GetStats(access_token!)));
    }
}