namespace SecondHand.Library.Queries.TokenExchange;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetTokenExchangeByIdQuery(long? id) 
    : IRequest<SecondHand.Library.Models.Strava.TokenExchange>;