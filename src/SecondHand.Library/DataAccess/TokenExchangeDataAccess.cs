namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecondHand.Library.Models.Strava;
public class TokenExchangeDataAccess : ITokenExchangeDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    private readonly IMongoCollection<TokenExchange> _TokenExchangeCollection;
    IConfiguration _configuration;

    public TokenExchangeDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
        var mongoClient = new MongoClient(_configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(_configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _TokenExchangeCollection = mongoDatabase.GetCollection<TokenExchange>(_configuration.GetSection("SecondHandDatabase").GetSection("TokenExchangeCollectionName").Value);
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
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context?.TokenExchange?.Add(TokenExchange);
            _context?.SaveChanges();
            return TokenExchange;
        }
    }

    public TokenExchange UpdateTokenExchange(TokenExchange tokenExchange)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            TokenExchange model = _context?.TokenExchange?.FirstOrDefault(p => p.Id == tokenExchange.Id)!;
            if (model != null)
            {
                model.AccessToken = tokenExchange.AccessToken;
                model.ExpiresIn = tokenExchange.ExpiresIn;
                model.RefreshToken = tokenExchange.RefreshToken;
                model.TokenType = tokenExchange.TokenType;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("TokenExchange not found");
            }
            return model!;
        }
        
    }

    public bool DeleteTokenExchange(long? id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            TokenExchange model = _context?.TokenExchange?.FirstOrDefault(p => p.Id == id)!;
            if (model != null)
            {
                _context?.Remove(model);
                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("TokenExchange not found");
            }
        }
    }
}