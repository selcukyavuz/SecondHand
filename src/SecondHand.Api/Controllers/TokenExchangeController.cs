using Microsoft.AspNetCore.Mvc;
using SecondHand.Library.Models.Strava;
using SecondHand.Library.Queries.TokenExchange;
using SecondHand.Library.Commands.TokenExchange;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Events;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenExchangeController : ControllerBase
{   
    private readonly ILogger<TokenExchangeController> _logger;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 
    private readonly IMongoCollection<TokenExchange> _TokenExchangeCollection;

    public TokenExchangeController(
        ILogger<TokenExchangeController> logger, 
        IMediator mediator,
        IConfiguration configuration,
        IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _configuration = configuration;
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _TokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(
            secondHandDatabaseSettings.Value.TokenExchangeCollectionName);
    }

    [HttpGet()]
    public async Task<List<TokenExchange>> Get() => await _mediator.Send(new GetTokenExchangeListQuery());

    [HttpGet("{id}")]
    public async Task<TokenExchange> Get(long? id) => await _mediator.Send(new GetTokenExchangeByIdQuery(id));

    [HttpPost()]
    public async Task<TokenExchange> Post([FromBody] TokenExchange value)
    {
        TokenExchange TokenExchange = await _mediator.Send(new InsertTokenExchangeCommand(value!));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION") 
            ?? 
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new TokenExchangeCreatedEvent(Guid.NewGuid(), value));
        }

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
