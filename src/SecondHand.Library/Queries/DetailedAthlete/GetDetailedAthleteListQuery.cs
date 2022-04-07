namespace SecondHand.Library.Queries.DetailedAthlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetDetailedAthleteListQuery : IRequest<List<DetailedAthlete>>;