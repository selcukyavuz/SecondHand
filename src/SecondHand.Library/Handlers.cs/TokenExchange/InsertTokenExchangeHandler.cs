using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class InsertTokenExchangeHandler : 
    IRequestHandler<InsertTokenExchangeCommand, SecondHand.Library.Models.Strava.TokenExchange>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public InsertTokenExchangeHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.Strava.TokenExchange> Handle(
        InsertTokenExchangeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertTokenExchange(request.TokenExchange));
    }
}