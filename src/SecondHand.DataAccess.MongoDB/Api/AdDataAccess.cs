namespace SecondHand.DataAccess.MongoDB.Api;

using System.Collections.Generic;
using global::MongoDB.Driver;
using SecondHand.Models.Advertisement;
using Microsoft.Extensions.Configuration;
using SecondHand.DataAccess.MongoDB.Interface;

public class AdDataAccess : DataAccessBase<Ad>, IAdDataAccess
{
    public AdDataAccess(IConfiguration configuration) : base(configuration,"AdCollectionName")
    {
    }

    public List<Ad> GetAd() => Collection.Find(_ => true).ToList();

    public Ad GetAd(int id) => Collection.Find(x => x.Id == id).FirstOrDefault();

    public Ad InsertAd(Ad Ad) => throw new NotImplementedException();

    public Ad UpdateAd(Ad Ad) => throw new NotImplementedException();

    public bool DeleteAd(int id) => throw new NotImplementedException();
}