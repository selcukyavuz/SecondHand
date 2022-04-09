using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class GetAthleteListQueryHandler : IRequestHandler<GetAthleteListQuery, List<SecondHand.Library.Models.Strava.Athlete>>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public GetAthleteListQueryHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<SecondHand.Library.Models.Strava.Athlete>> Handle(GetAthleteListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetAthlete());
    }
}