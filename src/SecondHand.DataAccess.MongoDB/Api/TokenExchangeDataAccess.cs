namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Strava;
using SecondHand.DataAccess.MongoDB.Interface;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;

public class TokenExchangeDataAccess : DataAccessBase<TokenExchange>, ITokenExchangeDataAccess
{
    private const string _collectionName = "TokenExchange";

    public TokenExchangeDataAccess(IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings) : base(secondHandDatabaseSettings, _collectionName)
    {
    }

    public List<TokenExchange> GetTokenExchange() =>  Collection.Find(_ => true).ToList();

    public TokenExchange GetTokenExchange(long? id) => Collection.Find(x => x.Id == id).FirstOrDefault();

    public TokenExchange InsertTokenExchange(TokenExchange TokenExchange) => throw new NotImplementedException();

    public TokenExchange UpdateTokenExchange(TokenExchange TokenExchange) =>  throw new NotImplementedException();

    public bool DeleteTokenExchange(int id) => throw new NotImplementedException();
}