using SecondHand.Api.Client.Adapter;

namespace SecondHand.Api.Client
{
    public interface ISecondHandApiClient
    {
        AthleteAdapter Athlete();
        TokenExchangeAdapter TokenExchange();

    }
}