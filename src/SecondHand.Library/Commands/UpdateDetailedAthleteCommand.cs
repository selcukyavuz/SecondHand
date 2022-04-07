namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record UpdateDetailedAthleteCommand(DetailedAthlete detailedAthlete) : IRequest<DetailedAthlete>;