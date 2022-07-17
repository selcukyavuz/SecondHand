using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class GetAdListQueryHandler : IRequestHandler<GetAdListQuery, List<Models.Advertisement.Ad>>
{
    private readonly IAdDataAccess _dataAccess;

    public GetAdListQueryHandler(IAdDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<List<Models.Advertisement.Ad>> Handle(GetAdListQuery request, CancellationToken cancellationToken) => Task.FromResult(_dataAccess.GetAd());
}