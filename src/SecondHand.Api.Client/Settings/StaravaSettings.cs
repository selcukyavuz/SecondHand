namespace SecondHand.Api.Client.Settings;

public class StravaSettings
{
    public const string Key = "Strava";
    public string ClientId { get; set; } = String.Empty;
    public string ClientSecret { get; set; } = String.Empty;
    public string TokenExchangeUrl { get; set; } = String.Empty;
}