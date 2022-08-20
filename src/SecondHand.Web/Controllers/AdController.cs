namespace SecondHand.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Api.Client;
using SecondHand.Models.Advertisement;
using SecondHand.Web.ViewModel;

public class AdController : BaseController
{
    private const string create = "~/ad/create";
    public AdController(IConfiguration configuration) : base(configuration)
    {
    }

    [HttpGet(create)]
    public async Task<IActionResult> Create()
    {
        var categories = await Task.Run(() => SecondHandApiClient.Category().Get());
        var products = await Task.Run(() => SecondHandApiClient.Product().Get());
        var marks = await Task.Run(() => SecondHandApiClient.Mark().Get());

        var createAdViewModel = new CreateAdViewModel(){
            Categories = categories,
            Products = products,
            Marks = marks
        };

        return View(createAdViewModel);
    }

    [HttpPost(create)]
    public IActionResult Create([FromBody] Ad ad)
    {
        if (ad is null)
        {
            throw new ArgumentNullException(nameof(ad));
        }

        return View();
    }
}