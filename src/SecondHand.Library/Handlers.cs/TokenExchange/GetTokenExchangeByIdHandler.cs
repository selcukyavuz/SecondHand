using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
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
    {
        return Task.FromResult(_dataAccess.GetTokenExchange(request.id));
    }
}