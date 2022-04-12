using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Queries.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class GetAthleteByIdHandler : IRequestHandler<GetAthleteByIdQuery, SecondHand.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public GetAthleteByIdHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Strava.Athlete> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetAthlete(request.Id));
    }
}