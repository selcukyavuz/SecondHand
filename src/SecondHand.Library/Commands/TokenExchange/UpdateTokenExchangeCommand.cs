namespace SecondHand.Library.Commands.DetailedAthlete;

using SecondHand.Library.Models;
using MediatR;

public record UpdateTokenExchangeCommand(TokenExchange tokenExchange) : IRequest<TokenExchange>;