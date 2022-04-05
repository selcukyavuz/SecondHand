using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands;

namespace SecondHand.Library.Handlers;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, bool>
{ 
    private readonly IDataAccess _dataAccess;

    public DeletePersonHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.DeletePerson(request.id));
    }
}