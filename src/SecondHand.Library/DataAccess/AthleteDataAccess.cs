namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecondHand.Library.Models.Strava;
public class AthleteDataAccess : IAthleteDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    private readonly IMongoCollection<Athlete> _athleteCollection;
    IConfiguration _configuration;

    public AthleteDataAccess(
        IDbContextFactory<SecondHandContext> contextFactory,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
        var mongoClient = new MongoClient(
            _configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(
            _configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _athleteCollection = mongoDatabase.GetCollection<Athlete>(
            _configuration.GetSection("SecondHandDatabase").GetSection("AthleteCollectionName").Value);
    }

    public List<Athlete> GetAthlete()
    {
        return _athleteCollection.Find(_ => true).ToList();
    }

    public Athlete GetAthlete(int id)
    {
        return _athleteCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public Athlete InsertAthlete(Athlete athlete)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context?.Athlete?.Add(athlete);
            _context?.SaveChanges();
            return athlete;
        }
    }

    public Athlete UpdateAthlete(Athlete athlete)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Athlete model = _context?.Athlete?.FirstOrDefault(p => p.Id == athlete.Id)!;
            if (model != null)
            {
                model.FirstName = athlete.FirstName;
                model.LastName = athlete.LastName;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("Athlete not found");
            }
            return model!;
        }
        
    }

    public bool DeleteAthlete(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Athlete model = _context?.Athlete?
                .Include(b=>b.Bikes)
                .Include(c=>c.Clubs)
                .Include(s=>s.Shoes)
                .FirstOrDefault(p => p.Id == id)!;

            if (model != null)
            {
                _context?.Remove(model);
                if(model.Bikes != null)
                {
                    foreach(var bike in model.Bikes)
                    {
                        _context?.Remove(bike);
                    }
                }

                if(model.Clubs != null)
                {
                    foreach(var club in model.Clubs)
                    {
                        _context?.Remove(club);
                    }
                }

                if(model.Shoes != null)
                {
                    foreach(var shoe in model.Shoes)
                    {
                        _context?.Remove(shoe);
                    }
                }
                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Athlete not found");
            }
        }
    }
}