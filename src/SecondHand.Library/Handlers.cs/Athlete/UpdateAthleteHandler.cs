using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class UpdateAthleteHandler : IRequestHandler<UpdateAthleteCommand, SecondHand.Library.Models.Strava.Athlete>
{ 
    private readonly IAthleteDataAccess _dataAccess;

    public UpdateAthleteHandler(IAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.Strava.Athlete> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateAthlete(request.athlete));
    }
}