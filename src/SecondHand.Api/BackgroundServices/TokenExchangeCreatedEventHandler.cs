using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Models.Strava;
using SecondHand.Library.Events;
using SecondHand.Models.Settings;

namespace SecondHand.Api.BackgroundServices
{
    public class TokenExchangeCreatedEventHandler : BackgroundService
    {
        private readonly IMongoCollection<TokenExchange> _tokenExchangeCollection;
        private readonly RabbitSettings _rabbitSettings;

        public TokenExchangeCreatedEventHandler(
            IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings,
            IOptions<RabbitSettings> rabbitSettings)
        {
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _tokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(nameof(TokenExchange));
            _rabbitSettings = rabbitSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IBus _bus = RabbitHutch.CreateBus(_rabbitSettings.Connection);
            _bus.PubSub.Subscribe<TokenExchangeCreatedEvent>("NewTokenExchangeEventHandler", ProcessTokenExchange, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }

        private void ProcessTokenExchange(TokenExchangeCreatedEvent TokenExchangeCreatedEvent)
            => _tokenExchangeCollection.InsertOne(
                    TokenExchangeCreatedEvent.TokenExchange
                );
    }
}
