using MediatR;
using SecondHandGear.Library.DataAccess;
using SecondHandGear.Library.Models;
using SecondHandGear.Library.Queries;

namespace SecondHandGear.Library.Handlers;

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