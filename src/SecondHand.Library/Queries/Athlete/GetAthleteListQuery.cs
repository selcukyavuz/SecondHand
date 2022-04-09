namespace SecondHand.Library.Queries.Athlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetAthleteListQuery : IRequest<List<Athlete>>;