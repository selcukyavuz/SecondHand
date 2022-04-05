using MediatR;
using SecondHandGear.Library.DataAccess;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Queries;

namespace SecondHandGear.Library.Handlers;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
{ 
    private readonly IDataAccess _dataAccess;

    public GetPersonByIdHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetPeople(request.id));
    }
}