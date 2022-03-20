using Microsoft.AspNetCore.Mvc;
using StravaStore.Data;

namespace StravaStore.Controllers;

public class AthletesController : Controller
{
    private readonly StravaStoreClient _StravaStoreClient = new StravaStoreClient(
        "", 
        "", 
        "https://www.strava.com/api/v3");

    private readonly ILogger<AthletesController> _logger;
    private readonly StravaStoreContext _context;
    private readonly IHttpContextAccessor _accessor;

    public AthletesController(
        ILogger<AthletesController> logger,
        StravaStoreContext context,
        IHttpContextAccessor accessor
        )
    {
        _logger = logger;
        _context = context;
        _accessor = accessor;
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
        return View(await Task.Run(()=> _StravaStoreClient.Athlete().GetStats(access_token!)));
    }
}