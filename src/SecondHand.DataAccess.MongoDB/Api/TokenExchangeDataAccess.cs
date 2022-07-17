namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Strava;
using Microsoft.Extensions.Configuration;
using SecondHand.DataAccess.MongoDB.Interface;

public class TokenExchangeDataAccess : DataAccessBase<TokenExchange>, ITokenExchangeDataAccess
{
    private const string _collectionName = "TokenExchangeCollectionName";

    public TokenExchangeDataAccess(IConfiguration configuration) : base(configuration, _collectionName)
    {
    }

    public List<TokenExchange> GetTokenExchange() =>  Collection.Find(_ => true).ToList();

    public TokenExchange GetTokenExchange(long? id) => Collection.Find(x => x.Id == id).FirstOrDefault();

    public TokenExchange InsertTokenExchange(TokenExchange TokenExchange) => throw new NotImplementedException();

    public TokenExchange UpdateTokenExchange(TokenExchange TokenExchange) =>  throw new NotImplementedException();

    public bool DeleteTokenExchange(int id) => throw new NotImplementedException();
}