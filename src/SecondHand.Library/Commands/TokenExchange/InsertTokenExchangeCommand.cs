namespace SecondHand.Library.Commands.TokenExchange;

using MediatR;
using SecondHand.Models.Strava;

public record InsertTokenExchangeCommand(TokenExchange TokenExchange) : IRequest<TokenExchange>;