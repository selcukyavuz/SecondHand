using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class DeleteTokenExchangeHandler : IRequestHandler<DeleteTokenExchangeCommand, bool>
{
    private readonly ITokenExchangeDataAccess _dataAccess;

    public DeleteTokenExchangeHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<bool> Handle(DeleteTokenExchangeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.DeleteTokenExchange(request.id));
    }
}