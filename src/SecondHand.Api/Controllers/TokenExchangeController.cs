namespace SecondHand.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SecondHand.Models.Strava;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Queries.TokenExchange;
using SecondHand.Library.Commands.TokenExchange;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class TokenExchangeController : Controller
{
    private readonly IMediator _mediator;
    private readonly RabbitSettings _rabbitSettings;
    public TokenExchangeController(IMediator mediator,IOptions<RabbitSettings> rabbitSettings)
    {
        _mediator = mediator;
        _rabbitSettings = rabbitSettings.Value;
    }

    [HttpGet()]
    public async Task<List<TokenExchange>> Get() => await _mediator.Send(new GetTokenExchangeListQuery());

    [HttpGet("{id}")]
    public async Task<TokenExchange> Get(long? id) => await _mediator.Send(new GetTokenExchangeByIdQuery(id));

    [HttpPost()]
    public async Task<TokenExchange> Post([FromBody] TokenExchange value)
    {
        TokenExchange TokenExchange = await _mediator.Send(new InsertTokenExchangeCommand(value!));

        RabbitHutch.CreateBus(_rabbitSettings.Connection).PubSub.Publish(new TokenExchangeCreatedEvent(Guid.NewGuid(), value));

        return TokenExchange;
    }

    [HttpPut()]
    public async Task<TokenExchange> Put([FromBody] TokenExchange value) => await _mediator.Send(new UpdateTokenExchangeCommand(value));

    [HttpDelete("{id}")]
    public async Task<bool> Delete(long? id) => await _mediator.Send(new DeleteTokenExchangeCommand(id));
}
