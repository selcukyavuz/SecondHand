using MediatR;
using SecondHand.DataAccess.MongoDB.Api;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeByIdHandler : IRequestHandler<GetTokenExchangeByIdQuery, SecondHand.Models.Strava.TokenExchange>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeByIdHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Strava.TokenExchange> Handle(GetTokenExchangeByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.GetTokenExchange(request.Id));
}