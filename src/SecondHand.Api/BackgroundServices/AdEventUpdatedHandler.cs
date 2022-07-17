using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Advertisement;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class AdEventUpdatedHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Ad> _AdCollection;
        public AdEventUpdatedHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _AdCollection = mongoDatabase.GetCollection<Ad>(secondHandDatabaseSettings.Value.AdCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
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
