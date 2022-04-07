using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.DetailedAthlete;

namespace SecondHand.Library.Handlers.DetailedAthlete;

public class UpdateDetailedAthleteHandler : IRequestHandler<UpdateDetailedAthleteCommand, SecondHand.Library.Models.DetailedAthlete>
{ 
    private readonly IDetailedAthleteDataAccess _dataAccess;

    public UpdateDetailedAthleteHandler(IDetailedAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.DetailedAthlete> Handle(UpdateDetailedAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateDetailedAthlete(request.detailedAthlete));
    }
}