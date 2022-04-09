using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class GetAthleteByIdHandler : IRequestHandler<GetAthleteByIdQuery, SecondHand.Library.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public GetAthleteByIdHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.Strava.Athlete> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetAthlete(request.Id));
    }
}