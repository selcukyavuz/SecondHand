namespace SecondHand.Library.Commands.DetailedAthlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record UpdateDetailedAthleteCommand(DetailedAthlete detailedAthlete) : IRequest<DetailedAthlete>;