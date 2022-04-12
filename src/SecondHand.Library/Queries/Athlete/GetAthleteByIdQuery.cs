namespace SecondHand.Library.Queries.Athlete;

using SecondHand.Models.Strava;
using MediatR;

public record GetAthleteByIdQuery(int Id) : IRequest<Athlete>;