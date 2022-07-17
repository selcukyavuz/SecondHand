namespace SecondHand.DataAccess.MongoDB.Api;

using global::MongoDB.Driver;
using Microsoft.Extensions.Configuration;

public class DataAccessBase<T>
{
    protected const string SecondHandDatabase = "SecondHandDatabase";
    protected const string ConnectionString = "ConnectionString";
    protected const string DatabaseName = "DatabaseName";
    protected IMongoDatabase MongoDatabase;
    protected readonly IMongoCollection<T> Collection;

    public DataAccessBase(IConfiguration configuration, string collectionName)
    {
        var mongoClient = new MongoClient(configuration.GetSection(SecondHandDatabase).GetSection(ConnectionString).Value);
        MongoDatabase = mongoClient.GetDatabase(configuration.GetSection(SecondHandDatabase).GetSection(DatabaseName).Value);
        Collection = MongoDatabase.GetCollection<T>(configuration.GetSection(SecondHandDatabase).GetSection(collectionName).Value);
    }
}