using Microsoft.AspNetCore.Mvc;
using SecondHand.Library.Models;
using SecondHand.Library.Queries;
using SecondHand.Library.Commands;
using MediatR;
using EasyNetQ;
using SecondHand.Library.Events;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;

namespace SecondHand.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{   
    private readonly ILogger<PersonController> _logger;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 
    private readonly IMongoCollection<PersonModel> _PeopleCollection;

    public PersonController(ILogger<PersonController> logger, IMediator mediator,IConfiguration configuration,IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        _logger = logger;
        _mediator = mediator;
        _configuration = configuration;
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _PeopleCollection = mongoDatabase.GetCollection<PersonModel>(secondHandDatabaseSettings.Value.PeopleCollectionName);
    }

    [HttpGet()]
    public async Task<List<PersonModel>> Get()
    {
        return await _mediator.Send(new GetPersonListQuery());
    }

    [HttpGet("{id}")]
    public async Task<PersonModel> Get(Guid id)
    {
        return await _mediator.Send(new GetPersonByIdQuery(id));
    }

    [HttpPost()]
    public async Task<PersonModel> Post([FromBody] PersonModel value)
    {
        PersonModel person = await _mediator.Send(new InsertPersonCommand(value.FirstName!,value.LastName!));

        using (var bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value))
        {
            bus.PubSub.Publish(new PersonCreatedEvent(Guid.NewGuid(), value));
        }

        return person;
    }

    [HttpPut()]
    public async Task<PersonModel> Put([FromBody] PersonModel value)
    {
        return await _mediator.Send(new UpdatePersonCommand(value.Id,value.FirstName!,value.LastName!));
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _mediator.Send(new DeletePersonCommand(id));
    }
}
