using Microsoft.AspNetCore.Mvc;
using SecondHand.Library.Models.Strava;
using SecondHand.Library.Queries.DetailedAthlete;
using SecondHand.Library.Commands.DetailedAthlete;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Events;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetailedAthleteController : ControllerBase
{   
    private readonly ILogger<DetailedAthleteController> _logger;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 
    private readonly IMongoCollection<DetailedAthlete> _DetailedAthleteCollection;

    public DetailedAthleteController(
        ILogger<DetailedAthleteController> logger, 
        IMediator mediator,
        IConfiguration configuration,
        IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _configuration = configuration;
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _DetailedAthleteCollection = mongoDatabase.GetCollection<DetailedAthlete>(
            secondHandDatabaseSettings.Value.DetailedAthleteCollectionName);
    }

    [HttpGet()]
    public async Task<List<DetailedAthlete>> Get()
    {
        return await _mediator.Send(new GetDetailedAthleteListQuery());
    }

    [HttpGet("{id}")]
    public async Task<DetailedAthlete> Get(int id)
    {
        return await _mediator.Send(new GetDetailedAthleteByIdQuery(id));
    }

    [HttpPost()]
    public async Task<DetailedAthlete> Post([FromBody] DetailedAthlete value)
    {
        DetailedAthlete detailedAthlete = await _mediator.Send(new InsertDetailedAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION") 
            ?? 
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new DetailedAthleteCreatedEvent(value.Id, value));
        }

        return detailedAthlete;
    }

    [HttpPut()]
    public async Task<DetailedAthlete> Put([FromBody] DetailedAthlete value)
    {
        DetailedAthlete detailedAthlete = await _mediator.Send(new UpdateDetailedAthleteCommand(value));

        using (var bus = RabbitHutch.CreateBus(
            Environment.GetEnvironmentVariable("RABBITCONNECTION") 
            ?? 
            _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new DetailedAthleteUpdatedEvent(value.Id, value));
        }

        return detailedAthlete;
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        bool result = await _mediator.Send(new DeleteDetailedAthleteCommand(id));

        using (var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new DetailedAthleteDeletedEvent(id));
        }

        return result;
    }
}
