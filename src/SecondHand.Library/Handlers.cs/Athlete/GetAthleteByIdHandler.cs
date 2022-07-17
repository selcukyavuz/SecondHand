using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Queries.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class GetAthleteByIdHandler : IRequestHandler<GetAthleteByIdQuery, Models.Strava.Athlete>
{
    private readonly IAthleteDataAccess _dataAccess;

    public GetAthleteByIdHandler(IAthleteDataAccess dataAccess) =>  _dataAccess = dataAccess;

    public Task<Models.Strava.Athlete> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.GetAthlete(request.Id));
}