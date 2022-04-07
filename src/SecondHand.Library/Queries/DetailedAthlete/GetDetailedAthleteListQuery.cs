namespace SecondHand.Library.Queries.DetailedAthlete;

using SecondHand.Library.Models;
using MediatR;

public record GetDetailedAthleteListQuery : IRequest<List<DetailedAthlete>>;