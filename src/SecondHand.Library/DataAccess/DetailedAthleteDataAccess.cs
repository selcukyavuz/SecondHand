namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecondHand.Library.Models;
public class DetailedAthleteDataAccess : IDetailedAthleteDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    private readonly IMongoCollection<DetailedAthlete> _detailedAthleteCollection;
    IConfiguration _configuration;

    public DetailedAthleteDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
        var mongoClient = new MongoClient(_configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(_configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _detailedAthleteCollection = mongoDatabase.GetCollection<DetailedAthlete>(_configuration.GetSection("SecondHandDatabase").GetSection("DetailedAthleteCollectionName").Value);
    }

    public List<DetailedAthlete> GetDetailedAthlete()
    {
        return _detailedAthleteCollection.Find(_ => true).ToList();
    }

    public DetailedAthlete GetDetailedAthlete(long? id)
    {
        return _detailedAthleteCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public DetailedAthlete InsertDetailedAthlete(DetailedAthlete detailedAthlete)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context?.DetailedAthlete?.Add(detailedAthlete);
            _context?.SaveChanges();
            return detailedAthlete;
        }
    }

    public DetailedAthlete UpdateDetailedAthlete(DetailedAthlete detailedAthlete)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            DetailedAthlete model = _context?.DetailedAthlete?.FirstOrDefault(p => p.Id == detailedAthlete.Id)!;
            if (model != null)
            {
                model.FirstName = detailedAthlete.FirstName;
                model.LastName = detailedAthlete.LastName;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("Person not found");
            }
            return model!;
        }
        
    }

    public bool DeleteDetailedAthlete(long? id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            DetailedAthlete model = _context?.DetailedAthlete?.FirstOrDefault(p => p.Id == id)!;
            if (model != null)
            {
                _context?.Remove(model);
                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("DetailedAthlete not found");
            }
        }
    }
}