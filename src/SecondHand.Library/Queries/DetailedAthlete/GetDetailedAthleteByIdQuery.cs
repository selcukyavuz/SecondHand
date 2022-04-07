namespace SecondHand.Library.Queries.DetailedAthlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetDetailedAthleteByIdQuery(long? id) : IRequest<DetailedAthlete>;