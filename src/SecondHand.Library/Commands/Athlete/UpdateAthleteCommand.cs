namespace SecondHand.Library.Commands.Athlete;

using SecondHand.Models.Strava;
using MediatR;

public record UpdateAthleteCommand(Athlete athlete) : IRequest<Athlete>;