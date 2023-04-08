using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Models.Advertisement;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;

namespace SecondHand.Api.BackgroundServices
{
    public class AdCreatedEventHandler : BackgroundService
    {
        private readonly IMongoCollection<Ad> _adCollection;
        private readonly RabbitSettings _rabbitSettings;

        public AdCreatedEventHandler(
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings,
            IOptions<RabbitSettings> rabbitSettings)
        {
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _adCollection = mongoDatabase.GetCollection<Ad>(nameof(Ad));
            _rabbitSettings = rabbitSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(_rabbitSettings.Connection);
            _ = _bus.PubSub.Subscribe<AdCreatedEvent>("NewAdEventHandler", ProcessAd, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProcessAd(AdCreatedEvent AdCreatedEvent) => _adCollection.InsertOne(AdCreatedEvent.Ad);
    }
}
