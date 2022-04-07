namespace SecondHand.Library.Queries;

using SecondHand.Library.Models;
using MediatR;

public record GetDetailedAthleteListQuery : IRequest<List<DetailedAthlete>>;