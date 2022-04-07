using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries.Person;

namespace SecondHand.Library.Handlers.Person;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
{ 
    private readonly IPersonDataAccess _dataAccess;

    public GetPersonByIdHandler(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetPeople(request.id));
    }
}