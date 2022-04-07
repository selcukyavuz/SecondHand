using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands.Person;

namespace SecondHand.Library.Handlers.Person;

public class InsertPersonHandler : IRequestHandler<InsertPersonCommand, PersonModel>
{ 
    private readonly IPersonDataAccess _dataAccess;

    public InsertPersonHandler(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertPerson(request.FirstName, request.LastName));
    }
}