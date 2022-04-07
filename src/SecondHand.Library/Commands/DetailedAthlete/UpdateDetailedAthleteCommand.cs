namespace SecondHand.Library.Commands.DetailedAthlete;

using SecondHand.Library.Models;
using MediatR;

public record UpdateDetailedAthleteCommand(DetailedAthlete detailedAthlete) : IRequest<DetailedAthlete>;