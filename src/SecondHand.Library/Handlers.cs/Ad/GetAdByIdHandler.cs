using MediatR;
using SecondHand.DataAccess.MongoDB.Api;
using SecondHand.Library.Queries.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class GetAdByIdHandler : IRequestHandler<GetAdByIdQuery, SecondHand.Models.Adversitement.Ad>
{ 
    private readonly IAdDataAccess _dataAccess;

    public GetAdByIdHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Adversitement.Ad> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetAd(request.Id));
    }
}