namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Strava;
using Microsoft.Extensions.Configuration;

public class AthleteDataAccess : IAthleteDataAccess
{
    private readonly IMongoCollection<Athlete> _athleteCollection;
    IConfiguration _configuration;

    public AthleteDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;

         var mongoClient = new MongoClient(
            _configuration.GetSection("SecondHandDatabase").GetSection("ConnectionString").Value);
        var mongoDatabase = mongoClient.GetDatabase(
            _configuration.GetSection("SecondHandDatabase").GetSection("DatabaseName").Value);
        _athleteCollection = mongoDatabase.GetCollection<Athlete>(
            _configuration.GetSection("SecondHandDatabase").GetSection("AthleteCollectionName").Value);
    }
    public bool DeleteAthlete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Athlete> GetAthlete()
    {
        return _athleteCollection.Find(_ => true).ToList();
    }

    public Athlete GetAthlete(int id)
    {
        return _athleteCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public Athlete InsertAthlete(Athlete athlete)
    {
        throw new NotImplementedException();
    }

    public Athlete UpdateAthlete(Athlete athlete)
    {
        throw new NotImplementedException();
    }
}