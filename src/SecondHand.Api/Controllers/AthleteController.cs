using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Strava;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.Athlete;
using SecondHand.Library.Commands.Athlete;
using SecondHand.Library.Events;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AthleteController : BaseController
{
    private readonly IMediator _mediator;
    public AthleteController(IMediator mediator, IConfiguration configuration) : base(configuration)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<Athlete>> Get() => await _mediator.Send(new GetAthleteListQuery());

    [HttpGet("{id}")]
    public async Task<Athlete> Get(int id) => await _mediator.Send(new GetAthleteByIdQuery(id));

    [HttpPost()]
    public async Task<Athlete> Post([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new InsertAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(ConnectionString))
        {
            bus.PubSub.Publish(new AthleteCreatedEvent(value.Id, value));
        }

        return athlete;
    }

    [HttpPut()]
    public async Task<Athlete> Put([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new UpdateAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(ConnectionString))
        {
            bus.PubSub.Publish(new AthleteUpdatedEvent(value.Id, value));
        }

        return athlete;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAthleteCommand(id));

        using (var bus = RabbitHutch.CreateBus(ConnectionString))
        {
            bus.PubSub.Publish(new AthleteDeletedEvent(id));
        }

        return result;
    }
}
