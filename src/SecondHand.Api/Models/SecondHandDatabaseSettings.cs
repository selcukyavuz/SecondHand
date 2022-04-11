namespace SecondHand.Api.Models;

public class SecondHandDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PeopleCollectionName { get; set; } = null!;

    public string AthleteCollectionName { get; set; } = null!;

    public string TokenExchangeCollectionName { get; set; } = null!;

}