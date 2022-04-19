using MediatR;
using SecondHand.DataAccess.SqlServer.Api;
using SecondHand.Library.Commands.Ad;

namespace SecondHand.Library.Handlers.Ad;

public class DeleteAdHandler : IRequestHandler<DeleteAdCommand, bool>
{ 
    private readonly IAdDataAccess _dataAccess;

    public DeleteAdHandler(IAdDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    public Task<bool> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dataAccess.DeleteAd(request.Id));
    }
}