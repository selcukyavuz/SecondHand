using MediatR;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class UpdateAthleteHandler : IRequestHandler<UpdateAthleteCommand, Models.Strava.Athlete>
{
    private readonly IAthleteDataAccess _dataAccess;

    public UpdateAthleteHandler(IAthleteDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Strava.Athlete> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken) => Task.FromResult(_dataAccess.UpdateAthlete(request.athlete));
}