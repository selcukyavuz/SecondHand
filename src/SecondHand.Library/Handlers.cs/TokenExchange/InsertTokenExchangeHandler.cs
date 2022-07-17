using MediatR;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class InsertTokenExchangeHandler : IRequestHandler<InsertTokenExchangeCommand, Models.Strava.TokenExchange>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public InsertTokenExchangeHandler(ITokenExchangeDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Strava.TokenExchange> Handle(InsertTokenExchangeCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.InsertTokenExchange(request.TokenExchange));
}