namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Library.Models;
using MediatR;

public record GetTokenExchangeByIdQuery(long? id) : IRequest<TokenExchange>;