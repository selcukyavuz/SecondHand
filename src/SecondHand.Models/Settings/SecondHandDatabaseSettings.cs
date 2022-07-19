namespace SecondHand.Models.Settings;

public class SecondHandDatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string PeopleCollectionName { get; set; } = string.Empty;
    public string AthleteCollectionName { get; set; } = string.Empty;
    public string TokenExchangeCollectionName { get; set; } = string.Empty;
    public string AdCollectionName { get; set; } = string.Empty;

}