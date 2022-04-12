namespace SecondHand.Library.Commands.TokenExchange;

using MediatR;
using SecondHand.Models.Strava;

public record UpdateTokenExchangeCommand(TokenExchange TokenExchange) : IRequest<TokenExchange>;