namespace SecondHand.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;

public class AthletesController : BaseController
{    private readonly IHttpContextAccessor _accessor;

    public AthletesController(IConfiguration configuration,IHttpContextAccessor accessor) : base(configuration)
    {
        _accessor = accessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("~/athlete/detail")]
    public async Task<IActionResult> GetStats()
    {
        var AthleteId = Convert.ToInt32(_accessor.HttpContext?.Session.GetString("AthleteId"));
        var athlete = await Task.Run(() => SecondHandApiClient.Athlete().Get(AthleteId));
        return View(athlete);
    }
}