using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.DetailedAthlete;

namespace SecondHand.Library.Handlers.DetailedAthlete;

public class InsertDetailedAthleteHandler : IRequestHandler<InsertDetailedAthleteCommand, SecondHand.Library.Models.DetailedAthlete>
{ 
    private readonly IDetailedAthleteDataAccess _dataAccess;

    public InsertDetailedAthleteHandler(IDetailedAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.DetailedAthlete> Handle(InsertDetailedAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertDetailedAthlete(request.detailedAthlete));
    }
}