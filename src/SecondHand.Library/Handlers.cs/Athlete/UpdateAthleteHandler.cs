using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class UpdateAthleteHandler : IRequestHandler<UpdateAthleteCommand, SecondHand.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public UpdateAthleteHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Strava.Athlete> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateAthlete(request.athlete));
    }
}