namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Adversitement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
public class MarkDataAccess : IMarkDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    IConfiguration _configuration;

    public MarkDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
    }

    public List<Mark> GetMark()
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Mark?.ToList()!;
        }
    }

    public Mark GetMark(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Mark?.Where(x => x.Id == id).FirstOrDefault()!;
        }
    }

    public Mark InsertMark(Mark category)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context?.Mark?.Add(category);
            _context?.SaveChanges();
            return category;
        }
    }

    public Mark UpdateMark(Mark category)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Mark model = _context?.Mark?.FirstOrDefault(p => p.Id == category.Id)!;
            if (model != null)
            {
                model.Name = category.Name;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("Mark not found");
            }
            return model!;
        }
        
    }

    public bool DeleteMark(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Mark model = _context?.Mark?.FirstOrDefault(p => p.Id == id)!;

            if (model != null)
            {
                _context?.Remove(model);

                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Mark not found");
            }
        }
    }
}