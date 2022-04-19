namespace SecondHand.Library.Commands.Ad;

using MediatR;

public record DeleteAdCommand(int Id) : IRequest<bool>;