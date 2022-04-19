using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Library.Queries.Ad;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Events;
using SecondHand.Models.Adversitement;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdController : ControllerBase
{   
    private readonly ILogger<AdController> _logger;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 
    private readonly IMongoCollection<Ad> _AdCollection;

    public AdController(
        ILogger<AdController> logger, 
        IMediator mediator,
        IConfiguration configuration,
        IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _configuration = configuration;
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _AdCollection = mongoDatabase.GetCollection<Ad>(
            secondHandDatabaseSettings.Value.AdCollectionName);
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

        using (var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AdDeletedEvent(id));
        }

        return result;
    }
}
