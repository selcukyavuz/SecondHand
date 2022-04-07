using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands.Person;

namespace SecondHand.Library.Handlers.Person;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, PersonModel>
{ 
    private readonly IPersonDataAccess _dataAccess;

    public UpdatePersonHandler(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdatePerson(request.id,request.FirstName, request.LastName));
    }
}