using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class InsertAthleteHandler : IRequestHandler<InsertAthleteCommand, SecondHand.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public InsertAthleteHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Strava.Athlete> Handle(InsertAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertAthlete(request.athlete));
    }
}