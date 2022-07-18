namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Strava;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.Athlete;
using SecondHand.Library.Commands.Athlete;
using SecondHand.Library.Events;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;

[ApiController]
[Route("api/[controller]")]
public class AthleteController : Controller
{
    private readonly IMediator _mediator;
    private readonly RabbitSettings _rabbitSettings;
    public AthleteController(IMediator mediator,IOptions<RabbitSettings> rabbitSettings)
    {
        _mediator = mediator;
        _rabbitSettings = rabbitSettings.Value;
    }

    [HttpGet()]
    public async Task<List<Athlete>> Get() => await _mediator.Send(new GetAthleteListQuery());

    [HttpGet("{id}")]
    public async Task<Athlete> Get(int id) => await _mediator.Send(new GetAthleteByIdQuery(id));

    [HttpPost()]
    public async Task<Athlete> Post([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new InsertAthleteCommand(value));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AthleteCreatedEvent(value.Id, value));

        return athlete;
    }

    [HttpPut()]
    public async Task<Athlete> Put([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new UpdateAthleteCommand(value));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AthleteUpdatedEvent(value.Id, value));

        return athlete;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAthleteCommand(id));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AthleteDeletedEvent(id));

        return result;
    }
}
