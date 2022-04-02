using Microsoft.AspNetCore.Mvc;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Queries;
using SecondHandGear.Library.Commands;
using MediatR;

namespace SecondHandGear.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{    private readonly ILogger<PersonController> _logger;
    private readonly IMediator _mediator;


    public PersonController(ILogger<PersonController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<PersonModel>> Get()
    {
        return await _mediator.Send(new GetPersonListQuery());
    }

    [HttpGet("{id}")]
    public async Task<PersonModel> Get(int id)
    {
        return await _mediator.Send(new GetPersonByIdQuery(id));
    }

    [HttpPost()]
    public async Task<PersonModel> Post([FromBody] PersonModel value)
    {
        return await _mediator.Send(new InsertPersonCommand(value.FirstName!,value.LastName!));
    }
}
