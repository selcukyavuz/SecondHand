using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.DetailedAthlete;

namespace SecondHand.Library.Handlers.DetailedAthlete;

public class GetDetailedAthleteListQueryHandler : IRequestHandler<GetDetailedAthleteListQuery, List<SecondHand.Library.Models.DetailedAthlete>>
{ 
    private readonly IDetailedAthleteDataAccess _dataAccess;

    public GetDetailedAthleteListQueryHandler(IDetailedAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<SecondHand.Library.Models.DetailedAthlete>> Handle(GetDetailedAthleteListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetDetailedAthlete());
    }
}