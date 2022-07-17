namespace SecondHand.Library.Queries.TokenExchange;

using MediatR;

public record GetTokenExchangeByIdQuery(long? Id) : IRequest<Models.Strava.TokenExchange>;