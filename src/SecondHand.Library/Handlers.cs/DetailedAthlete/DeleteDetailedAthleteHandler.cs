using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.DetailedAthlete;

namespace SecondHand.Library.Handlers.DetailedAthlete;

public class DeleteDetailedAthleteHandler : IRequestHandler<DeleteDetailedAthleteCommand, bool>
{ 
    private readonly IDetailedAthleteDataAccess _dataAccess;

    public DeleteDetailedAthleteHandler(IDetailedAthleteDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<bool> Handle(DeleteDetailedAthleteCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.DeleteDetailedAthlete(request.id));
    }
}