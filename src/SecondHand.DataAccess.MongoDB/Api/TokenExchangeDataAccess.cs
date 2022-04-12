namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Strava;
using Microsoft.Extensions.Configuration;

public class TokenExchangeDataAccess : ITokenExchangeDataAccess
{
    private readonly IMongoCollection<TokenExchange> _TokenExchangeCollection;
    IConfiguration _configuration;

    public TokenExchangeDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;

         var mongoClient = new MongoClient(
            _configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(
            _configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _TokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(
            _configuration.GetSection("SecondHandDatabase").GetSection("TokenExchangeCollectionName").Value);
    }
    public bool DeleteTokenExchange(int id)
    {
        throw new NotImplementedException();
    }

    public List<TokenExchange> GetTokenExchange()
    {
        return _TokenExchangeCollection.Find(_ => true).ToList();
    }

    public TokenExchange GetTokenExchange(long? id)
    {
        return _TokenExchangeCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public TokenExchange InsertTokenExchange(TokenExchange TokenExchange)
    {
        throw new NotImplementedException();
    }

    public TokenExchange UpdateTokenExchange(TokenExchange TokenExchange)
    {
        throw new NotImplementedException();
    }
}