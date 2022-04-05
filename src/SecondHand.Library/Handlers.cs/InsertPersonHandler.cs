using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands;

namespace SecondHand.Library.Handlers;

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