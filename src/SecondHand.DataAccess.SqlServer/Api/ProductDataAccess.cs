namespace SecondHand.DataAccess.SqlServer.Api;

using System.Collections.Generic;
using SecondHand.Models.Adversitement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
public class ProductDataAccess : IProductDataAccess
{
    private readonly IDbContextFactory<SecondHandContext> _contextFactory;
    IConfiguration _configuration;

    public ProductDataAccess(IDbContextFactory<SecondHandContext> contextFactory,IConfiguration configuration)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context.Database.EnsureCreated();
        }
    }

    public List<Product> GetProduct()
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Product?.ToList()!;
        }
    }

    public Product GetProduct(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            return _context?.Product?.Where(x => x.Id == id).FirstOrDefault()!;
        }
    }

    public Product InsertProduct(Product category)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            _context?.Product?.Add(category);
            _context?.SaveChanges();
            return category;
        }
    }

    public Product UpdateProduct(Product category)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Product model = _context?.Product?.FirstOrDefault(p => p.Id == category.Id)!;
            if (model != null)
            {
                model.Name = category.Name;
                _context?.SaveChanges();
            }
            else
            {
                throw new Exception("Product not found");
            }
            return model!;
        }
        
    }

    public bool DeleteProduct(int id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            Product model = _context?.Product?.FirstOrDefault(p => p.Id == id)!;

            if (model != null)
            {
                _context?.Remove(model);

                _context?.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}