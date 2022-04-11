using SecondHand.Library.Events;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Library.Models.Strava;

namespace SecondHand.Api.BackgroundServices
{
    public class NewAthleteEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Athlete> _athleteCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public NewAthleteEventHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _athleteCollection = mongoDatabase.GetCollection<Athlete>(secondHandDatabaseSettings.Value.AthleteCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<AthleteCreatedEvent>("NewAthleteEventHandler", ProccessAthlete);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessAthlete(AthleteCreatedEvent athleteCreatedEvent)
        {
            _athleteCollection.InsertOne(
                athleteCreatedEvent.Athlete
                );
        }
    }
}