using SecondHand.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecondHand.Library.Models;

namespace SecondHand.Services;

public class PeopleService
{
    private readonly IMongoCollection<PersonModel> _PeopleCollection;

    public PeopleService(IOptions<SecondHandDatabaseSettings> secondHandDatabaseSettings)
    {
        var mongoClient = new MongoClient(secondHandDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(secondHandDatabaseSettings.Value.DatabaseName);
        _PeopleCollection = mongoDatabase.GetCollection<PersonModel>(secondHandDatabaseSettings.Value.PeopleCollectionName);
    }

    public async Task<List<PersonModel>> GetAsync() => await _PeopleCollection.Find(_ => true).ToListAsync();

    public async Task<PersonModel?> GetAsync(Guid id) => await _PeopleCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PersonModel newPerson) => await _PeopleCollection.InsertOneAsync(newPerson);

    public async Task UpdateAsync(Guid id, PersonModel updatedBook) => await _PeopleCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(Guid id) => await _PeopleCollection.DeleteOneAsync(x => x.Id == id);
}


   