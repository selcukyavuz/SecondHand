using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StravaAuth.Common;
using StravaAuth.Response;

namespace StravaAuth.Controllers;

public class AthletesController : Controller
{
    private readonly StravaAuthClient _stravaAuthClient = new StravaAuthClient(
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
        return View(await Task.Run(()=> _stravaAuthClient.Athlete().GetStats(HttpContext.Session.GetString("access_token")!)));
    }
}