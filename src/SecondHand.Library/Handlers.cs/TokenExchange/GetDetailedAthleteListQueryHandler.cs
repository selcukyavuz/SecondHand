using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeListQueryHandler : IRequestHandler<GetTokenExchangeListQuery, List<SecondHand.Library.Models.Strava.TokenExchange>>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeListQueryHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<SecondHand.Library.Models.Strava.TokenExchange>> Handle(GetTokenExchangeListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetTokenExchange());
    }
}