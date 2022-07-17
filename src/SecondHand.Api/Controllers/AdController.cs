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
public class AdController : BaseController
{
    private readonly IMediator _mediator;
    public AdController(IMediator mediator,IConfiguration configuration) : base(configuration)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<Ad>> Get() => await _mediator.Send(new GetAdListQuery());

    [HttpGet("{id}")]
    public async Task<Ad> Get(int id) => await _mediator.Send(new GetAdByIdQuery(id));

    [HttpPost()]
    public async Task<Ad> Post([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new InsertAdCommand(value));

        RabbitHutch.CreateBus(ConnectionString).PubSub.Publish(new AdCreatedEvent(value.Id, value));

        return Ad;
    }

    [HttpPut()]
    public async Task<Ad> Put([FromBody] Ad value)
    {
        Ad Ad = await _mediator.Send(new UpdateAdCommand(value));

        RabbitHutch.CreateBus(ConnectionString).PubSub.Publish(new AdUpdatedEvent(value.Id, value));

        return Ad;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAdCommand(id));

        RabbitHutch.CreateBus(ConnectionString).PubSub.Publish(new AdDeletedEvent(id));

        return result;
    }
}
