namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Advertisement;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.SqlServer.Interface;

public class CategoryDataAccess : ICategoryDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    public CategoryDataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using var _context = _contextFactory.CreateDbContext();
        _context.Database.EnsureCreated();
    }

    public List<Category> GetCategory()
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Category?.ToList()!;
    }

    public Category GetCategory(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Category?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public Category InsertCategory(Category category)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context?.Category?.Add(category);
        _context?.SaveChanges();
        return category;
    }

    public Category UpdateCategory(Category category)
    {
        using var _context = _contextFactory.CreateDbContext();
        Category model = _context?.Category?.FirstOrDefault(p => p.Id == category.Id)!;
        if (model != null)
        {
            model.Name = category.Name;
            _context?.SaveChanges();
        }
        else
        {
            throw new Exception("Category not found");
        }
        return model!;
    }

    public bool DeleteCategory(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        Category model = _context?.Category?.FirstOrDefault(p => p.Id == id)!;

        if (model != null)
        {
            _context?.Remove(model);

            _context?.SaveChanges();
            return true;
        }
        else
        {
            throw new Exception("Category not found");
        }
    }
}