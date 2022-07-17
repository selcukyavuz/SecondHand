using MediatR;
using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class UpdateAdHandler : IRequestHandler<UpdateAdCommand, Models.Advertisement.Ad>
{
    private readonly IAdDataAccess _dataAccess;

    public UpdateAdHandler(IAdDataAccess dataAccess) => _dataAccess = dataAccess;

    public Task<Models.Advertisement.Ad> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        => Task.FromResult(_dataAccess.UpdateAd(request.Ad));
}