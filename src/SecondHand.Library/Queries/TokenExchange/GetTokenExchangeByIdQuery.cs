namespace SecondHand.Library.Queries.TokenExchange;

using MediatR;

public record GetTokenExchangeByIdQuery(long? Id) : IRequest<SecondHand.Models.Strava.TokenExchange>;