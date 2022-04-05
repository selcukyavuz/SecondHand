namespace SecondHand.Library.DataAccess;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecondHand.Library.Models;
public class DataAccess : IDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;

    public DataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
    }

    public List<PersonModel> GetPeople()
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
            return _context?.People?.ToList()!;
        }
    }

    public PersonModel GetPeople(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
            return _context?.People?.FirstOrDefault(p => p.Id == id)!;
        }
        
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

    public PersonModel UpdatePerson(int id,string firstName,string lastName)
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

    public bool DeletePerson(int id)
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