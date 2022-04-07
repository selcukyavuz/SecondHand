using SecondHand.Library.Events;
using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Library.Models;

namespace SecondHand.Api.BackgroundServices
{
    public class NewTokenExchangeHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<TokenExchange> _tokenExchangeCollection;
        private IOptions<SecondHandDatabaseSettings> _SecondHandDatabaseSettings;

        public NewTokenExchangeHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            _SecondHandDatabaseSettings = secondHandDatabaseSettings;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _tokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(secondHandDatabaseSettings.Value.TokenExchangeCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<TokenExchangeCreatedEvent>("NewTokenExchangeHandler", ProccessTokenExchange);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProccessTokenExchange(TokenExchangeCreatedEvent TokenExchangeCreatedEvent)
        {
            _tokenExchangeCollection.InsertOne(
                TokenExchangeCreatedEvent.TokenExchange
                );
        }
    }
}
