using MediatR;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class InsertAthleteHandler : IRequestHandler<InsertAthleteCommand, Models.Strava.Athlete>
{
    private readonly IAthleteDataAccess _dataAccess;

    public InsertAthleteHandler(IAthleteDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Strava.Athlete> Handle(InsertAthleteCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.InsertAthlete(request.Athlete));
}