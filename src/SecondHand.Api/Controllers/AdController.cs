namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.Ad;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Events;
using SecondHand.Models.Advertisement;

[ApiController]
[Route("api/[controller]")]
public class AdController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    public AdController(
        IMediator mediator,
        IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpGet()]
    public async Task<List<Ad>> Get() => await _mediator.Send(new GetAdListQuery());

    [HttpGet("{id}")]
    public async Task<Ad> Get(int id) => await _mediator.Send(new GetAdByIdQuery(id));

    [HttpPost()]
    public async Task<Ad> Post([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new InsertAdCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION")
            ??
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AdCreatedEvent(value.Id, value));
        }

        return Ad;
    }

    [HttpPut()]
    public async Task<Ad> Put([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new UpdateAdCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION")
            ??
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AdUpdatedEvent(value.Id, value));
        }

        return Ad;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAdCommand(id));

        using (var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION")
        ??
        _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AdDeletedEvent(id));
        }

        return result;
    }
}
