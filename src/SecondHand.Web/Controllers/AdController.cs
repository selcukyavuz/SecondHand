using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;
using SecondHand.Models.Adversitement;
using SecondHand.Web.ViewModel;

namespace SecondHand.Web.Controllers;

public class AdController : Controller
{
    private readonly ILogger<AdController> _logger;
    private readonly IConfiguration _configuration;
    private readonly SecondHandWebClient _SecondHandWebClient;
    private readonly SecondHandApiClient _secondHandApiClient;
    private readonly IHttpContextAccessor _accessor;

    public AdController(ILogger<AdController> logger,IConfiguration configuration,IHttpContextAccessor accessor)
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

    [HttpGet("~/ad/create")]
    public async Task<IActionResult> Create()
    {
        var categories = await Task.Run(() => _secondHandApiClient.Category().Get());
        var products = await Task.Run(() => _secondHandApiClient.Product().Get());
        var marks = await Task.Run(() => _secondHandApiClient.Mark().Get());

        var model = new CreateAdViewModel(){ 
            Categories = categories,
            Products = products,
            Marks = marks
        };

        return View(model);
    }

    [HttpPost("~/ad/create")]
    public  IActionResult Create([FromBody] Ad Ad)
    {
        
        //Add an Ad
        return View();
    }

    
}