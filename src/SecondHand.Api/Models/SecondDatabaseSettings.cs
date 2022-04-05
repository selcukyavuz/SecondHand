namespace SecondHand.Api.Models;

public class SecondHandDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PeopleCollectionName { get; set; } = null!;

    public string BooksCollectionName { get; set; } = null!;
}