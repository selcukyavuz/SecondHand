using Microsoft.AspNetCore.Mvc;

namespace SecondHand.Web.Controllers;
using SecondHand.Library.Models;
using MediatR;
using RestSharp;
using System.Text.Json;
using SecondHand.Web.Common;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IMediator _mediator;

    public PeopleController(ILogger<PeopleController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var tokenExchangeClient = new RestClient("https://localhost:7269/api/TokenExchange");
        RestRequest request = new RestRequest();
        RestResponse restResponseTokenExchange = await tokenExchangeClient.ExecuteGetAsync(request);
        PersonModel? responseTokenExchange = JsonSerializer.Deserialize<PersonModel>(restResponseTokenExchange.Content!,SecondHandWebJsonSerializerSettings.Settings);
        return View(responseTokenExchange);
    }
}
