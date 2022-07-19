using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Models.Advertisement;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;

namespace SecondHand.Api.BackgroundServices
{
    public class AdEventUpdatedHandler : BackgroundService
    {
        private readonly IMongoCollection<Ad> _AdCollection;
        private readonly RabbitSettings _rabbitSettings;
        public AdEventUpdatedHandler(
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings,
            IOptions<RabbitSettings> rabbitSettings)
        {
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _AdCollection = mongoDatabase.GetCollection<Ad>(nameof(Ad));
            _rabbitSettings = rabbitSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(_rabbitSettings.Connection);
            _bus.PubSub.Subscribe<AdUpdatedEvent>("UpdateAdEventHandler", ProcessAd, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessAd(AdUpdatedEvent AdUpdatedEvent)
        {
            var filter = Builders<Ad>.Filter.Eq(s => s.Id, AdUpdatedEvent.Ad.Id);
            _AdCollection.ReplaceOneAsync(
                filter,
                AdUpdatedEvent.Ad
            );
        }
    }
}
