using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class UpdateAdHandler : IRequestHandler<UpdateAdCommand, SecondHand.Models.Advertisement.Ad>
{
    private readonly IAdDataAccess _dataAccess;

    public UpdateAdHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Advertisement.Ad> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateAd(request.Ad));
    }
}