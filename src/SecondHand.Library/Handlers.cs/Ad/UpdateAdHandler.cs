using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class UpdateAdHandler : IRequestHandler<UpdateAdCommand, SecondHand.Models.Adversitement.Ad>
{ 
    private readonly IAdDataAccess _dataAccess;

    public UpdateAdHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<SecondHand.Models.Adversitement.Ad> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.UpdateAd(request.ad));
    }
}