using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries;

namespace SecondHand.Library.Handlers;

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