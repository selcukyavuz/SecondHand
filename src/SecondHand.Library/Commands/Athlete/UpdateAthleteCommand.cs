namespace SecondHand.Library.Commands.Athlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record UpdateAthleteCommand(Athlete athlete) : IRequest<Athlete>;