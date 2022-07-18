using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Strava;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;

namespace SecondHand.Api.BackgroundServices
{
    public class AthleteDeletedEventHandler : BackgroundService
    {
        private readonly IMongoCollection<Athlete> _athleteCollection;
        private readonly RabbitSettings _rabbitSettings;

        public AthleteDeletedEventHandler(
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings,
            IOptions<RabbitSettings> rabbitSettings)
        {
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _athleteCollection = mongoDatabase.GetCollection<Athlete>(secondHandDatabaseSettings.Value.AthleteCollectionName);
            _rabbitSettings = rabbitSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(
                Environment.GetEnvironmentVariable("RABBITCONNECTION")
                ??
                _rabbitSettings.Connection);
            _ = _bus.PubSub.Subscribe<AthleteDeletedEvent>("DeleteAthleteEventHandler", ProcessAthlete, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessAthlete(AthleteDeletedEvent athleteDeletedEvent)
            => _athleteCollection.DeleteOne(
                    Builders<Athlete>.Filter.Eq(s => s.Id, athleteDeletedEvent.Id)
                    );
    }
}
