using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SecondHand.Library.Models.Strava;
using SecondHand.Web.Common;

namespace SecondHand.Web.Controllers;

public class AthletesController : Controller
{
    private readonly SecondHandWebClient _SecondHandWebClient = new SecondHandWebClient(
        "", 
        "", 
        "https://www.strava.com/api/v3");

    private readonly ILogger<AthletesController> _logger;

    public AthletesController(ILogger<AthletesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("~/athlete/stats")]
    public async Task<IActionResult> GetStats()
    {
        var athleteId = 1;
        var tokenExchangeClient = new RestClient("https://localhost:7269/api/DetailedAthlete/" + athleteId );
        RestRequest restRequest = new RestRequest();
        RestResponse restResponse = await tokenExchangeClient.ExecuteGetAsync(restRequest);
        DetailedAthlete? detailedAthlete = JsonSerializer.Deserialize<DetailedAthlete>(restResponse.Content!,SecondHandWebJsonSerializerSettings.Settings);
        return View(detailedAthlete);
        
        // #pragma warning disable CS8602
        // var access_token = _context.TokenPools?.FirstOrDefault(c=>c.SessionID == HttpContext.Session.Id).AccessToken;
        // #pragma warning restore CS8602
        // return View(await Task.Run(()=> _SecondHandWebClient.Athlete().GetStats(access_token!)));
    }
}