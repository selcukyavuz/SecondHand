using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Strava;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class AthleteEventUpdatedHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Athlete> _athleteCollection;
        public AthleteEventUpdatedHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _athleteCollection = mongoDatabase.GetCollection<Athlete>(secondHandDatabaseSettings.Value.AthleteCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<AthleteUpdatedEvent>("UpdateAthleteEventHandler", ProcessAthlete, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessAthlete(AthleteUpdatedEvent athleteUpdatedEvent)
        {
            var filter = Builders<Athlete>.Filter.Eq(s => s.Id, athleteUpdatedEvent.Athlete.Id);
            _athleteCollection.ReplaceOneAsync(
                filter,
                athleteUpdatedEvent.Athlete
            );
        }
    }
}
