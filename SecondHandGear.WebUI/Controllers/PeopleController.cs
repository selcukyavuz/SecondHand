using Microsoft.AspNetCore.Mvc;

namespace StravaStore.Controllers;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Handlers;
using SecondHandGear.Library.Queries;
using MediatR;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IMediator _mediator;

    public PeopleController(ILogger<PeopleController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public IActionResult Index()
    {         
        System.Threading.Tasks.Task<List<PersonModel>> people =  _mediator.Send(new GetPersonListQuery());
        return View(people.Result);
    }
}
