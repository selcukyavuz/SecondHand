namespace SecondHand.Library.Commands.TokenExchange;

using MediatR;
using SecondHand.Library.Models.Strava;

public record UpdateTokenExchangeCommand(TokenExchange TokenExchange) : IRequest<TokenExchange>;