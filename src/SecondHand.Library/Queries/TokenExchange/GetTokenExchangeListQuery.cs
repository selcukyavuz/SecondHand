namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Models.Strava;
using MediatR;

public record GetTokenExchangeListQuery : IRequest<List<TokenExchange>>;