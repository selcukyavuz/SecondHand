namespace SecondHand.Library.Commands.TokenExchange;

using SecondHand.Library.Models;
using MediatR;

public record UpdateTokenExchangeCommand(TokenExchange TokenExchange) : IRequest<TokenExchange>;