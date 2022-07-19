using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Models.Advertisement;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;

namespace SecondHand.Api.BackgroundServices
{
    public class AdDeletedEventHandler : BackgroundService
    {
        private readonly IMongoCollection<Ad>? _AdCollection;
        private readonly RabbitSettings _rabbitSettings;

        public AdDeletedEventHandler(
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
            _ = _bus.PubSub.Subscribe<AdDeletedEvent>("DeleteAdEventHandler", ProcessAd, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessAd(AdDeletedEvent AdDeletedEvent)
        {
            var filter = Builders<Ad>.Filter.Eq(s => s.Id, AdDeletedEvent.Id);
            _ = (_AdCollection?.DeleteOne(
                filter
            ));
        }
    }
}
