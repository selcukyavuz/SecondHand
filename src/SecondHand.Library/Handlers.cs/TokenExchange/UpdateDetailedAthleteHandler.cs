using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class UpdateTokenExchangeHandler : 
    IRequestHandler<UpdateTokenExchangeCommand, SecondHand.Library.Models.Strava.TokenExchange>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public UpdateTokenExchangeHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.Strava.TokenExchange> Handle(
        UpdateTokenExchangeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateTokenExchange(request.TokenExchange));
    }
}