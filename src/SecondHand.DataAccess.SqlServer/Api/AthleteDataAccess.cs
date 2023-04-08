namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Strava;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.SqlServer.Interface;

public class AthleteDataAccess : IAthleteDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;

    public AthleteDataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using var _context = _contextFactory.CreateDbContext();
        _context.Database.EnsureCreated();
    }

    public List<Athlete> GetAthlete()
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Athlete?.ToList()!;
    }

    public Athlete GetAthlete(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Athlete?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public Athlete InsertAthlete(Athlete athlete)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context?.Athlete?.Add(athlete);
        _context?.SaveChanges();
        return athlete;
    }

    public Athlete UpdateAthlete(Athlete athlete)
    {
        using var _context = _contextFactory.CreateDbContext();
        Athlete model = _context?.Athlete?.FirstOrDefault(p => p.Id == athlete.Id)!;
        if (model != null)
        {
            model.FirstName = athlete.FirstName;
            model.LastName = athlete.LastName;
            _context?.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Athlete not found");
        }
        return model!;
    }

    public bool DeleteAthlete(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        Athlete model = _context?.Athlete?
            .Include(b => b.Bikes)
            .Include(c => c.Clubs)
            .Include(s => s.Shoes)
            .FirstOrDefault(p => p.Id == id)!;

        if (model != null)
        {
            _context?.Remove(model);

            if (model.Bikes != null)
            {
                _context?.RemoveRange(model.Bikes);
            }

            if (model.Clubs != null)
            {
                _context?.RemoveRange(model.Clubs);
            }

            if (model.Shoes != null)
            {
                _context?.RemoveRange(model.Shoes);
            }

            _context?.SaveChanges();

            return true;
        }
        else
        {
            throw new ArgumentException("Athlete not found");
        }
    }
}