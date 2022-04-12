namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Models.Strava;
using MediatR;

public record GetTokenExchangeByIdQuery(long? id) 
    : IRequest<SecondHand.Models.Strava.TokenExchange>;