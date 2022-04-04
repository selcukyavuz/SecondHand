using Microsoft.AspNetCore.Mvc;
using SecondHandGear.Web.Data;

namespace SecondHandGear.Web.Controllers;

public class AthletesController : Controller
{
    private readonly SecondHandGearWebClient _SecondHandGearWebClient = new SecondHandGearWebClient(
        "", 
        "", 
        "https://www.strava.com/api/v3");

    private readonly ILogger<AthletesController> _logger;
    private readonly SecondHandGearWebContext _context;

    public AthletesController(
        ILogger<AthletesController> logger,
        SecondHandGearWebContext context
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
        return View(await Task.Run(()=> _SecondHandGearWebClient.Athlete().GetStats(access_token!)));
    }
}