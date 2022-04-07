using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries.Person;

namespace SecondHand.Library.Handlers.Person;

public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<PersonModel>>
{ 
    private readonly IPersonDataAccess _dataAccess;

    public GetPersonListHandler(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetPeople());
    }
}