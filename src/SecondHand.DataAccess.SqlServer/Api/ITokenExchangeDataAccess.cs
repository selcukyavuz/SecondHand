namespace SecondHand.DataAccess.SqlServer.Api;

using SecondHand.Models.Strava;

public interface ITokenExchangeDataAccess
{
    public List<TokenExchange> GetTokenExchange();
    public TokenExchange GetTokenExchange(long? id);
    public TokenExchange InsertTokenExchange(TokenExchange TokenExchange);
    public TokenExchange UpdateTokenExchange(TokenExchange TokenExchange);
    public bool DeleteTokenExchange(long? id);
}
