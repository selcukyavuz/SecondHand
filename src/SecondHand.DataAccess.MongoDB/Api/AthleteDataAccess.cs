namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Strava;
using SecondHand.DataAccess.MongoDB.Interface;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;

public class AthleteDataAccess : DataAccessBase<Athlete>, IAthleteDataAccess
{
    private const string _collectionName = "Athlete";

    public AthleteDataAccess(IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings) : base(secondHandDatabaseSettings, _collectionName)
    {
    }
    public bool DeleteAthlete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Athlete> GetAthlete()
    {
        return Collection.Find(_ => true).ToList();
    }

    public Athlete GetAthlete(int id)
    {
        return Collection.Find(x => x.Id == id).FirstOrDefault();
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