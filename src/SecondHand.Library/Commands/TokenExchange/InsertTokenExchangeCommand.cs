namespace SecondHand.Library.Commands.TokenExchange;

using SecondHand.Library.Models;
using MediatR;

public record InsertTokenExchangeCommand(TokenExchange TokenExchange) : IRequest<TokenExchange>;