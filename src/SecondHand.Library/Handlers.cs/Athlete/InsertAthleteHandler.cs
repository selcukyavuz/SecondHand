using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class InsertAthleteHandler : IRequestHandler<InsertAthleteCommand, SecondHand.Library.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public InsertAthleteHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.Strava.Athlete> Handle(InsertAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertAthlete(request.athlete));
    }
}