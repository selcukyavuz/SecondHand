using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Queries.DetailedAthlete;

namespace SecondHand.Library.Handlers.DetailedAthlete;

public class GetDetailedAthleteByIdHandler : IRequestHandler<GetDetailedAthleteByIdQuery, SecondHand.Library.Models.DetailedAthlete>
{ 
    private readonly IDetailedAthleteDataAccess _dataAccess;

    public GetDetailedAthleteByIdHandler(IDetailedAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Library.Models.DetailedAthlete> Handle(GetDetailedAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetDetailedAthlete(request.id));
    }
}