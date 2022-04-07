namespace SecondHand.Library.Commands.DetailedAthlete;

using SecondHand.Library.Models;
using MediatR;

public record InsertDetailedAthleteCommand(DetailedAthlete detailedAthlete) : IRequest<DetailedAthlete>;