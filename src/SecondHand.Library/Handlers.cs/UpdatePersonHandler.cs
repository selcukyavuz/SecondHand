using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Commands;

namespace SecondHand.Library.Handlers;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, PersonModel>
{ 
    private readonly IDataAccess _dataAccess;

    public UpdatePersonHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdatePerson(request.id,request.FirstName, request.LastName));
    }
}