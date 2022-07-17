using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeByIdHandler : IRequestHandler<GetTokenExchangeByIdQuery, Models.Strava.TokenExchange>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeByIdHandler(ITokenExchangeDataAccess dataAccess) => _dataAccess = dataAccess;
    public Task<Models.Strava.TokenExchange> Handle(GetTokenExchangeByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.GetTokenExchange(request.Id));
}