namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Strava;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.TokenExchange;
using SecondHand.Library.Commands.TokenExchange;
using SecondHand.Library.Events;

[ApiController]
[Route("api/[controller]")]
public class TokenExchangeController : BaseController
{
    private readonly IMediator _mediator;
    public TokenExchangeController(IMediator mediator,IConfiguration configuration) : base(configuration)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<List<TokenExchange>> Get() => await _mediator.Send(new GetTokenExchangeListQuery());

    [HttpGet("{id}")]
    public async Task<TokenExchange> Get(long? id) => await _mediator.Send(new GetTokenExchangeByIdQuery(id));

    [HttpPost()]
    public async Task<TokenExchange> Post([FromBody] TokenExchange value)
    {
        TokenExchange TokenExchange = await _mediator.Send(new InsertTokenExchangeCommand(value!));

        RabbitHutch.CreateBus(ConnectionString).PubSub.Publish(new TokenExchangeCreatedEvent(Guid.NewGuid(), value));

        return TokenExchange;
    }

    [HttpPut()]
    public async Task<TokenExchange> Put([FromBody] TokenExchange value) => await _mediator.Send(new UpdateTokenExchangeCommand(value));

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long? id)
    {
        return await _mediator.Send(new DeleteTokenExchangeCommand(id));
    }
}
