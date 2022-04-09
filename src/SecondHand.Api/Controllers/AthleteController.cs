using Microsoft.AspNetCore.Mvc;
using SecondHand.Library.Models.Strava;
using SecondHand.Library.Queries.Athlete;
using SecondHand.Library.Commands.Athlete;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Events;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AthleteController : ControllerBase
{   
    private readonly ILogger<AthleteController> _logger;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 
    private readonly IMongoCollection<Athlete> _AthleteCollection;

    public AthleteController(
        ILogger<AthleteController> logger, 
        IMediator mediator,
        IConfiguration configuration,
        IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _configuration = configuration;
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _AthleteCollection = mongoDatabase.GetCollection<Athlete>(
            secondHandDatabaseSettings.Value.AthleteCollectionName);
    }

    [HttpGet()]
    public async Task<List<Athlete>> Get()
    {
        return await _mediator.Send(new GetAthleteListQuery());
    }

    [HttpGet("{id}")]
    public async Task<Athlete> Get(int id)
    {
        return await _mediator.Send(new GetAthleteByIdQuery(id));
    }

    [HttpPost()]
    public async Task<Athlete> Post([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new InsertAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION") 
            ?? 
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AthleteCreatedEvent(value.Id, value));
        }

        return athlete;
    }

    [HttpPut()]
    public async Task<Athlete> Put([FromBody] Athlete value)
    {
        Athlete athlete = await _mediator.Send(new UpdateAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION") 
            ?? 
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AthleteUpdatedEvent(value.Id, value));
        }

        return athlete;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteAthleteCommand(id));

        using (var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new AthleteDeletedEvent(id));
        }

        return result;
    }
}
