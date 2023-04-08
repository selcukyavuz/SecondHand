namespace SecondHand.DataAccess.SqlServer.Interface;

using SecondHand.Models.Strava;

public interface ITokenExchangeDataAccess
{
    public List<TokenExchange> GetTokenExchange();
    public TokenExchange GetTokenExchange(long? id);
    public TokenExchange InsertTokenExchange(TokenExchange tokenExchange);
    public TokenExchange UpdateTokenExchange(TokenExchange tokenExchange);
    public bool DeleteTokenExchange(long? id);
}
