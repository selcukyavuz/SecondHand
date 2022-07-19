namespace SecondHand.DataAccess.MongoDB.Api;

using global::MongoDB.Driver;
using Microsoft.Extensions.Options;
using SecondHand.Models.Settings;

public class DataAccessBase<T>
{
    protected const string SecondHandDatabase = "SecondHandDatabase";
    protected const string ConnectionString = "ConnectionString";
    protected const string DatabaseName = "DatabaseName";
    protected IMongoDatabase MongoDatabase;
    protected readonly IMongoCollection<T> Collection;

    public DataAccessBase(IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings, string collectionName)
    {
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        MongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        Collection = MongoDatabase.GetCollection<T>(collectionName);
    }
}