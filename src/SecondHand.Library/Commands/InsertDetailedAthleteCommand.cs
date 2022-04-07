namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record InsertDetailedAthleteCommand(DetailedAthlete detailedAthlete) : IRequest<DetailedAthlete>;