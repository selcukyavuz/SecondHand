namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Advertisement;
using Microsoft.EntityFrameworkCore;
using SecondHand.DataAccess.SqlServer.Interface;

public class ProductDataAccess : IProductDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;

    public ProductDataAccess(IDbContextFactory<SecondHandContext> contextFactory)
    {
        _contextFactory = contextFactory;
        using var _context = _contextFactory.CreateDbContext();
        _context.Database.EnsureCreated();
    }

    public List<Product> GetProduct()
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Product?.ToList()!;
    }

    public Product GetProduct(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context?.Product?.Where(x => x.Id == id).FirstOrDefault()!;
    }

    public Product InsertProduct(Product category)
    {
        using var _context = _contextFactory.CreateDbContext();
        _context?.Product?.Add(category);
        _context?.SaveChanges();
        return category;
    }

    public Product UpdateProduct(Product category)
    {
        using var _context = _contextFactory.CreateDbContext();
        Product model = _context?.Product?.FirstOrDefault(p => p.Id == category.Id)!;
        if (model != null)
        {
            model.Name = category.Name;
            _context?.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Product not found");
        }
        return model!;
    }

    public bool DeleteProduct(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        Product model = _context?.Product?.FirstOrDefault(p => p.Id == id)!;

        if (model != null)
        {
            _context?.Remove(model);

            _context?.SaveChanges();
            return true;
        }
        else
        {
            throw new ArgumentException("Product not found");
        }
    }
}