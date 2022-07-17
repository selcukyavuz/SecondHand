using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Advertisement;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class AdCreatedEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Ad> _AdCollection;

        public AdCreatedEventHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _AdCollection = mongoDatabase.GetCollection<Ad>(secondHandDatabaseSettings.Value.AdCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION")
            ??
             _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _ = _bus.PubSub.Subscribe<AdCreatedEvent>("NewAdEventHandler", ProcessAd, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessAd(AdCreatedEvent AdCreatedEvent)
        {
            _AdCollection.InsertOne(AdCreatedEvent.Ad);
        }
    }
}
