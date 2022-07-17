using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Commands.Athlete;

namespace SecondHand.Library.Handlers.Athlete;

public class DeleteAthleteHandler : IRequestHandler<DeleteAthleteCommand, bool>
{
    private readonly IAthleteDataAccess _dataAccess;

    public DeleteAthleteHandler(IAthleteDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<bool> Handle(DeleteAthleteCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.DeleteAthlete(request.Id));
}