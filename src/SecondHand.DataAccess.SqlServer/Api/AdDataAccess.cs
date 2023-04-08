namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Advertisement;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.SqlServer.Interface;

public class AdDataAccess : IAdDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;

    public AdDataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using var _context = _contextFactory.CreateDbContext();
        _context.Database.EnsureCreated();
    }

    public List<Ad> GetAd()
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Ad?.ToList()!;
    }

    public Ad GetAd(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Ad?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public Ad InsertAd(Ad Ad)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context?.Ad?.Add(Ad);
        _context?.SaveChanges();
        return Ad;
    }

    public Ad UpdateAd(Ad Ad)
    {
        using var _context = _contextFactory.CreateDbContext();
        Ad model = _context?.Ad?.FirstOrDefault(p => p.Id == Ad.Id)!;
        if (model != null)
        {
            model.Category = Ad.Category;
            model.City = Ad.City;
            model.Country = Ad.Country;
            model.Mark = Ad.Mark;
            model.ModelYear = Ad.ModelYear;
            model.Price = Ad.Price;
            model.PriceCurrency = Ad.PriceCurrency;
            model.Product = Ad.Product;
            model.PublishDate = Ad.PublishDate;
            model.State = Ad.State;
            model.Subject = Ad.Subject;
            _context?.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Ad not found");
        }
        return model!;
    }

    public bool DeleteAd(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        Ad model = _context?.Ad?.FirstOrDefault(p => p.Id == id)!;

        if (model != null)
        {
            _context?.Remove(model);
            _context?.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentException("Ad not found");
        }
    }
}