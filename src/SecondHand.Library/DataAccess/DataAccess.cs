namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecondHand.Library.Models;
public class DataAccess : IDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    private readonly IMongoCollection<PersonModel> _peopleCollection;
    private readonly IMongoCollection<DetailedAthlete> _detailedAthleteCollection;
    IConfiguration _configuration;

    public DataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
        var mongoClient = new MongoClient(_configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(_configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _peopleCollection = mongoDatabase.GetCollection<PersonModel>(_configuration.GetSection("SecondHandDatabase").GetSection("PeopleCollectionName").Value);
        _detailedAthleteCollection = mongoDatabase.GetCollection<DetailedAthlete>(_configuration.GetSection("SecondHandDatabase").GetSection("DetailedAthleteCollectionName").Value);
    }

    public List<PersonModel> GetPeople()
    {
        return _peopleCollection.Find(_ => true).ToList();
    }

    public PersonModel GetPeople(Guid id)
    {
        return _peopleCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public PersonModel InsertPerson(string firstName,string lastName)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            PersonModel model = new PersonModel{ FirstName = firstName, LastName = lastName };
            _context?.People?.Add(model);
            _context?.SaveChanges();
            return model;
        }
    }

    public PersonModel UpdatePerson(Guid id,string firstName,string lastName)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            PersonModel model = _context?.People?.FirstOrDefault(p => p.Id == id)!;
            if (model != null)
            {
                model.FirstName = firstName;
                model.LastName = lastName;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("Person not found");
            }
            return model!;
        }
        
    }

    public bool DeletePerson(Guid id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            PersonModel model = _context?.People?.FirstOrDefault(p => p.Id == id)!;
            if (model != null)
            {
                _context?.Remove(model);
                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Person not found");
            }
        }
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