using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Strava;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class AthleteDeletedEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Athlete> _athleteCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public AthleteDeletedEventHandler(
            IConfiguration configuration, 
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _athleteCollection = mongoDatabase.GetCollection<Athlete>(
                secondHandDatabaseSettings.Value.AthleteCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(
                Environment.GetEnvironmentVariable("RABBITCONNECTION") 
                ?? 
                _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<AthleteDeletedEvent>("DeleteAthleteEventHandler", ProccessAthlete);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessAthlete(AthleteDeletedEvent athleteDeletedEvent)
        {
            var filter = Builders<Athlete>.Filter.Eq(s => s.Id, athleteDeletedEvent.Id);
            _athleteCollection.DeleteOne(
                filter                
            );
        }
    }
}
