using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class GetAdByIdHandler : IRequestHandler<GetAdByIdQuery, SecondHand.Models.Advertisement.Ad>
{
    private readonly IAdDataAccess _dataAccess;

    public GetAdByIdHandler(IAdDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<SecondHand.Models.Advertisement.Ad> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.GetAd(request.Id));
}