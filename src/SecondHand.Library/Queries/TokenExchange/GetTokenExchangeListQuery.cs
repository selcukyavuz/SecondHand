namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetTokenExchangeListQuery : IRequest<List<TokenExchange>>;