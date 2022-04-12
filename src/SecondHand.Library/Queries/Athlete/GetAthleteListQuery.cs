namespace SecondHand.Library.Queries.Athlete;

using SecondHand.Models.Strava;
using MediatR;

public record GetAthleteListQuery : IRequest<List<Athlete>>;