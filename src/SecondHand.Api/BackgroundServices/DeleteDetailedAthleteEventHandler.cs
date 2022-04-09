using SecondHand.Library.Events;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Library.Models.Strava;

namespace SecondHand.Api.BackgroundServices
{
    public class DeleteDetailedAthleteEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<DetailedAthlete> _detailedAthleteCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public DeleteDetailedAthleteEventHandler(
            IConfiguration configuration, 
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _detailedAthleteCollection = mongoDatabase.GetCollection<DetailedAthlete>(
                secondHandDatabaseSettings.Value.DetailedAthleteCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(
                Environment.GetEnvironmentVariable("RABBITCONNECTION") 
                ?? 
                _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<DetailedAthleteDeletedEvent>("DeleteDetailedAthleteEventHandler", ProccessDetailedAthlete);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessDetailedAthlete(DetailedAthleteDeletedEvent detailedAthleteDeletedEvent)
        {
            var filter = Builders<DetailedAthlete>.Filter.Eq(s => s.Id, detailedAthleteDeletedEvent.Id);
            _detailedAthleteCollection.DeleteOne(
                filter                
            );
        }
    }
}
