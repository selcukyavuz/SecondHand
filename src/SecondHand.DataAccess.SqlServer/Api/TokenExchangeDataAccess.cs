namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Strava;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.SqlServer.Interface;

public class TokenExchangeDataAccess : ITokenExchangeDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;

    public TokenExchangeDataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using var _context = _contextFactory.CreateDbContext();
        _context.Database.EnsureCreated();
    }

    public List<TokenExchange> GetTokenExchange()
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.TokenExchange?.ToList()!;
    }

    public TokenExchange GetTokenExchange(long? id)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.TokenExchange?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public TokenExchange InsertTokenExchange(TokenExchange TokenExchange)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context?.TokenExchange?.Add(TokenExchange);
        _context?.SaveChanges();
        return TokenExchange;
    }

    public TokenExchange UpdateTokenExchange(TokenExchange tokenExchange)
    {
        using var _context = _contextFactory.CreateDbContext();
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

    public bool DeleteTokenExchange(long? id)
    {
        using var _context = _contextFactory.CreateDbContext();
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