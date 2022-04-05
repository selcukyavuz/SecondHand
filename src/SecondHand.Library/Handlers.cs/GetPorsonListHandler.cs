using MediatR;
using SecondHand.Library.DataAccess;
using SecondHand.Library.Models;
using SecondHand.Library.Queries;

namespace SecondHand.Library.Handlers;

public class GetPorsonListHandler : IRequestHandler<GetPersonListQuery, List<PersonModel>>
{ 
    private readonly IDataAccess _dataAccess;

    public GetPorsonListHandler(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
      public Task<List<PersonModel>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.GetPeople());
    }
}