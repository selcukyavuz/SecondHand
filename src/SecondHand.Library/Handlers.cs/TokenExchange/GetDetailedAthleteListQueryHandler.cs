using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeListQueryHandler : IRequestHandler<GetTokenExchangeListQuery, List<SecondHand.Models.Strava.TokenExchange>>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeListQueryHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<SecondHand.Models.Strava.TokenExchange>> Handle(GetTokenExchangeListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetTokenExchange());
    }
}