namespace SecondHand.Api.Client;

using SecondHand.Api.Client.Adapter;

public interface ISecondHandApiClient
{
    AthleteAdapter Athlete();
    TokenExchangeAdapter TokenExchange();
}