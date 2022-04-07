namespace SecondHand.Library.Queries;

using SecondHand.Library.Models;
using MediatR;

public record GetDetailedAthleteByIdQuery(long? id) : IRequest<DetailedAthlete>;