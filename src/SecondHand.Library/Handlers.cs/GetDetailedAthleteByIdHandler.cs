using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries;

namespace SecondHand.Library.Handlers;

public class GetDetailedAthleteByIdHandler : IRequestHandler<GetDetailedAthleteByIdQuery, DetailedAthlete>
{ 
    private readonly IDataAccess _dataAccess;

    public GetDetailedAthleteByIdHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<DetailedAthlete> Handle(GetDetailedAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetDetailedAthlete(request.id));
    }
}