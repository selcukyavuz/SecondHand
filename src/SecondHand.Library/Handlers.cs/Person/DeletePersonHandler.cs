using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Commands.Person;

namespace SecondHand.Library.Handlers.Person;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, bool>
{ 
    private readonly IPersonDataAccess _dataAccess;

    public DeletePersonHandler(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.DeletePerson(request.id));
    }
}