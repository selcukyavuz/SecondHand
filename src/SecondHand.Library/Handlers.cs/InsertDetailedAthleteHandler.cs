using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands;

namespace SecondHand.Library.Handlers;

public class InsertDetailedAthleteHandler : IRequestHandler<InsertDetailedAthleteCommand, DetailedAthlete>
{ 
    private readonly IDataAccess _dataAccess;

    public InsertDetailedAthleteHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<DetailedAthlete> Handle(InsertDetailedAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertDetailedAthlete(request.detailedAthlete));
    }
}