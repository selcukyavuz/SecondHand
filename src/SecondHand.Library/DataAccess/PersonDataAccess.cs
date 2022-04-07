namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecondHand.Library.Models;
public class PersonDataAccess : IPersonDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    private readonly IMongoCollection<PersonModel> _peopleCollection;
    IConfiguration _configuration;

    public PersonDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
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
}