using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class UpdateTokenExchangeHandler : IRequestHandler<UpdateTokenExchangeCommand, SecondHand.Models.Strava.TokenExchange>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public UpdateTokenExchangeHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Strava.TokenExchange> Handle(
        UpdateTokenExchangeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateTokenExchange(request.TokenExchange));
    }
}