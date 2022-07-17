namespace SecondHand.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;

public class AthletesController : Controller
{
    private readonly SecondHandApiClient _secondHandApiClient;
    private readonly IHttpContextAccessor _accessor;

    public AthletesController(IConfiguration configuration,IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _secondHandApiClient = new SecondHandApiClient(string.Empty,string.Empty,configuration["SecondHandApiUrl"]!);
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("~/athlete/detail")]
    public async Task<IActionResult> GetStats()
    {
        var AthleteId = Convert.ToInt32(_accessor.HttpContext?.Session.GetString("AthleteId"));
        var athlete = await Task.Run(() => _secondHandApiClient.Athlete().Get(AthleteId));
        return View(athlete);
    }
}