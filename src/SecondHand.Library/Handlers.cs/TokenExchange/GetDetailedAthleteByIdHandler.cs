using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.TokenExchange;

namespace SecondHand.Library.Handlers.TokenExchange;

public class GetTokenExchangeByIdHandler : IRequestHandler<GetTokenExchangeByIdQuery, SecondHand.Library.Models.TokenExchange>
{ 
    private readonly ITokenExchangeDataAccess _dataAccess;

    public GetTokenExchangeByIdHandler(ITokenExchangeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.TokenExchange> Handle(GetTokenExchangeByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetTokenExchange(request.id));
    }
}