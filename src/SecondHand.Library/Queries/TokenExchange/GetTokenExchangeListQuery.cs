namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Library.Models;
using MediatR;

public record GetTokenExchangeListQuery : IRequest<List<TokenExchange>>;