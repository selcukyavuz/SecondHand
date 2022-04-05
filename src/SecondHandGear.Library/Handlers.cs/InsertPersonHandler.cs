using MediatR;
using SecondHandGear.Library.DataAccess;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Commands;

namespace SecondHandGear.Library.Handlers;

public class InsertPersonHandler : IRequestHandler<InsertPersonCommand, PersonModel>
{ 
    private readonly IDataAccess _dataAccess;

    public InsertPersonHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertPerson(request.FirstName, request.LastName));
    }
}