namespace SecondHand.Library.Commands.TokenExchange;

using MediatR;

public record DeleteTokenExchangeCommand(long? Id) : IRequest<bool>;