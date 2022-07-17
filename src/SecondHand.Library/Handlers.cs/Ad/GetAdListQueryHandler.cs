using MediatR;
using SecondHand.DataAccess.MongoDB.Api;
using SecondHand.Library.Queries.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class GetAdListQueryHandler : IRequestHandler<GetAdListQuery, List<SecondHand.Models.Advertisement.Ad>>
{ 
    private readonly IAdDataAccess _dataAccess;

    public GetAdListQueryHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<SecondHand.Models.Advertisement.Ad>> Handle(GetAdListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetAd());
    }
}