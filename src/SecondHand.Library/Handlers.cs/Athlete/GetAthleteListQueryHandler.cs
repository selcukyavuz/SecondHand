using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class GetAthleteListQueryHandler : IRequestHandler<GetAthleteListQuery, List<Models.Strava.Athlete>>
{
    private readonly IAthleteDataAccess _dataAccess;

    public GetAthleteListQueryHandler(IAthleteDataAccess dataAccess) => _dataAccess = dataAccess;
    public Task<List<Models.Strava.Athlete>> Handle(GetAthleteListQuery request, CancellationToken cancellationToken) => Task.FromResult(_dataAccess.GetAthlete());
}