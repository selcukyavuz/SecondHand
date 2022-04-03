using MediatR;
using SecondHandGear.Library.DataAccess;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Commands;

namespace SecondHandGear.Library.Handlers;

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