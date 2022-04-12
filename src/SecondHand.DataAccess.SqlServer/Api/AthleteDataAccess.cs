namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.DataAccess.SqlServer;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecondHand.Models.Strava;
public class AthleteDataAccess : IAthleteDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    IConfiguration _configuration;

    public AthleteDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
    }

    public List<Athlete> GetAthlete()
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Athlete?.ToList()!;
        }
    }

    public Athlete GetAthlete(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Athlete?.Where(x => x.Id == id).FirstOrDefault()!;
        }
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