using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands;

namespace SecondHand.Library.Handlers;

public class UpdateDetailedAthleteHandler : IRequestHandler<UpdateDetailedAthleteCommand, DetailedAthlete>
{ 
    private readonly IDataAccess _dataAccess;

    public UpdateDetailedAthleteHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<DetailedAthlete> Handle(UpdateDetailedAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateDetailedAthlete(request.detailedAthlete));
    }
}