using EasyNetQ;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Api.Models;
using SecondHand.Models.Strava;
using SecondHand.Library.Events;

namespace SecondHand.Api.BackgroundServices
{
    public class TokenExchangeCreatedEventHandler : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<TokenExchange> _tokenExchangeCollection;

        public TokenExchangeCreatedEventHandler(IConfiguration configuration, IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
            _tokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(secondHandDatabaseSettings.Value.TokenExchangeCollectionName);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IBus _bus = RabbitHutch.CreateBus(Environment.GetEnvironmentVariable("RABBITCONNECTION") ?? _configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            _bus.PubSub.Subscribe<TokenExchangeCreatedEvent>("NewTokenExchangeEventHandler", ProcessTokenExchange, cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            _bus.Dispose();
        }

        private void ProcessTokenExchange(TokenExchangeCreatedEvent TokenExchangeCreatedEvent)
        {
            _tokenExchangeCollection.InsertOne(
                TokenExchangeCreatedEvent.TokenExchange
                );
        }
    }
}
