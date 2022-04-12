namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Adversitement;
using Microsoft.Extensions.Configuration;

public class AdDataAccess : IAdDataAccess
{
    private readonly IMongoCollection<Ad> _AdCollection;
    IConfiguration _configuration;

    public AdDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;

         var mongoClient = new MongoClient(
            _configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(
            _configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _AdCollection = mongoDatabase.GetCollection<Ad>(
            _configuration.GetSection("SecondHandDatabase").GetSection("AdCollectionName").Value);
    }
    public bool DeleteAd(int id)
    {
        throw new NotImplementedException();
    }

    public List<Ad> GetAd()
    {
        return _AdCollection.Find(_ => true).ToList();
    }

    public Ad GetAd(int id)
    {
        return _AdCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public Ad InsertAd(Ad Ad)
    {
        throw new NotImplementedException();
    }

    public Ad UpdateAd(Ad Ad)
    {
        throw new NotImplementedException();
    }
}