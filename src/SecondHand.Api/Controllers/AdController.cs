namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.Ad;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Events;
using SecondHand.Models.Advertisement;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;

[ApiController]
[Route("api/[controller]")]
public class AdController : Controller
{
    private readonly IMediator _mediator;
    private readonly RabbitSettings _rabbitSettings;
    public AdController(IMediator mediator, IOptions<RabbitSettings> rabbitSettings)
    {
        _mediator = mediator;
        _rabbitSettings = rabbitSettings.Value;
    }

    [HttpGet()]
    public async Task<List<Ad>> Get() => await _mediator.Send(new GetAdListQuery());

    [HttpGet("{id}")]
    public async Task<Ad> Get(int id) => await _mediator.Send(new GetAdByIdQuery(id));

    [HttpPost()]
    public async Task<Ad> Post([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new InsertAdCommand(value));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AdCreatedEvent(value.Id, value));

        return Ad;
    }

    [HttpPut()]
    public async Task<Ad> Put([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new UpdateAdCommand(value));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AdUpdatedEvent(value.Id, value));

        return Ad;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAdCommand(id));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new AdDeletedEvent(id));

        return result;
    }
}
