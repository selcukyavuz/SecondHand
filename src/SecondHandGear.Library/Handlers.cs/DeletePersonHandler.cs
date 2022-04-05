using MediatR;
using SecondHandGear.Library.DataAccess;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Commands;

namespace SecondHandGear.Library.Handlers;

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