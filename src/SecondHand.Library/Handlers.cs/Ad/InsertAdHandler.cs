using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class InsertAdHandler : IRequestHandler<InsertAdCommand, SecondHand.Models.Advertisement.Ad>
{
    private readonly IAdDataAccess _dataAccess;

    public InsertAdHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Advertisement.Ad> Handle(InsertAdCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.InsertAd(request.Ad));
    }
}