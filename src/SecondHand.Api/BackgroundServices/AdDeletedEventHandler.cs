using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Adversitement;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class AdDeletedEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Ad> _AdCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public AdDeletedEventHandler(
            IConfiguration configuration, 
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _AdCollection = mongoDatabase.GetCollection<Ad>(
                secondHandDatabaseSettings.Value.AdCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(
                Environment.GetEnvironmentVariable("RABBITCONNECTION") 
                ?? 
                _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<AdDeletedEvent>("DeleteAdEventHandler", ProccessAd);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessAd(AdDeletedEvent AdDeletedEvent)
        {
            var filter = Builders<Ad>.Filter.Eq(s => s.Id, AdDeletedEvent.Id);
            _AdCollection.DeleteOne(
                filter                
            );
        }
    }
}
