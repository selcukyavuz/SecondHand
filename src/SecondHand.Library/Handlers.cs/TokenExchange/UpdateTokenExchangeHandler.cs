using MediatR;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class UpdateTokenExchangeHandler : IRequestHandler<UpdateTokenExchangeCommand, Models.Strava.TokenExchange>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public UpdateTokenExchangeHandler(ITokenExchangeDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Strava.TokenExchange> Handle(UpdateTokenExchangeCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.UpdateTokenExchange(request.TokenExchange));
}