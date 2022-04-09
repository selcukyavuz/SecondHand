namespace SecondHand.Library.Queries.DetailedAthlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record GetDetailedAthleteByIdQuery(int Id) : IRequest<DetailedAthlete>;