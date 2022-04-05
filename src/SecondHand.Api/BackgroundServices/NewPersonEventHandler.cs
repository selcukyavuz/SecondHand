using SecondHand.Library.Events;
using SecondHand.Library.Models;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;

namespace SecondHand.Api.BackgroundServices
{
    public class NewPersonEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<PersonModel> _PeopleCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public NewPersonEventHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _PeopleCollection = mongoDatabase.GetCollection<PersonModel>(secondHandDatabaseSettings.Value.PeopleCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<PersonCreatedEvent>("NewPersonEventHandler", ProccessPerson);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessPerson(PersonCreatedEvent personCreatedEvent)
        {
            _PeopleCollection.InsertOne(
                new PersonModel{ FirstName = personCreatedEvent.PersonModel.FirstName, LastName = personCreatedEvent.PersonModel.LastName }
                );
        }
    }
}
