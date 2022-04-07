namespace SecondHand.Library.Commands.TokenExchange;

using MediatR;

public record DeleteTokenExchangeCommand(long? id) : IRequest<bool>;