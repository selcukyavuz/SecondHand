using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeListQueryHandler : IRequestHandler<GetTokenExchangeListQuery, List<Models.Strava.TokenExchange>>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeListQueryHandler(ITokenExchangeDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<List<Models.Strava.TokenExchange>> Handle(GetTokenExchangeListQuery request, CancellationToken cancellationToken) 
        => Task.FromResult(_dataAccess.GetTokenExchange());
}