using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;
using SecondHand.Models.Advertisement;
using SecondHand.Web.ViewModel;

namespace SecondHand.Web.Controllers;

public class AdController : Controller
{
    private readonly SecondHandApiClient _secondHandApiClient;

    public AdController(IConfiguration configuration)
    {
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

        var createAdViewModel = new CreateAdViewModel(){
            Categories = categories,
            Products = products,
            Marks = marks
        };

        return View(createAdViewModel);
    }

    [HttpPost("~/ad/create")]
    public IActionResult Create([FromBody] Ad ad)
    {
        if (ad is null)
        {
            throw new ArgumentNullException(nameof(ad));
        }

        return View();
    }
}