using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries;

namespace SecondHand.Library.Handlers;

public class GetDetailedAthleteListQueryHandler : IRequestHandler<GetDetailedAthleteListQuery, List<DetailedAthlete>>
{ 
    private readonly IDataAccess _dataAccess;

    public GetDetailedAthleteListQueryHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<DetailedAthlete>> Handle(GetDetailedAthleteListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetDetailedAthlete());
    }
}