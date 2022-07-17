using MediatR;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class InsertAdHandler : IRequestHandler<InsertAdCommand, Models.Advertisement.Ad>
{
    private readonly IAdDataAccess _dataAccess;

    public InsertAdHandler(IAdDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Advertisement.Ad> Handle(InsertAdCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.InsertAd(request.Ad));
}