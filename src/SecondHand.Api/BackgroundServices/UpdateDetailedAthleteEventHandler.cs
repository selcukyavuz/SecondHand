using SecondHand.Library.Events;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Library.Models.Strava;
using MongoDB.Bson;

namespace SecondHand.Api.BackgroundServices
{
    public class UpdateDetailedAthleteEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<DetailedAthlete> _detailedAthleteCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public UpdateDetailedAthleteEventHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _detailedAthleteCollection = mongoDatabase.GetCollection<DetailedAthlete>(secondHandDatabaseSettings.Value.DetailedAthleteCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<DetailedAthleteUpdatedEvent>("UpdateDetailedAthleteEventHandler", ProccessDetailedAthlete);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessDetailedAthlete(DetailedAthleteUpdatedEvent detailedAthleteUpdatedEvent)
        {
            var filter = Builders<DetailedAthlete>.Filter.Eq(s => s.Id, detailedAthleteUpdatedEvent.DetailedAthlete.Id);
            _detailedAthleteCollection.ReplaceOneAsync(
                filter,
                detailedAthleteUpdatedEvent.DetailedAthlete
            );
        }
    }
}
