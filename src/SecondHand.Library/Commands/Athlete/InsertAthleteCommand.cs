namespace SecondHand.Library.Commands.Athlete;

using SecondHand.Library.Models.Strava;
using MediatR;

public record InsertAthleteCommand(Athlete athlete) : IRequest<Athlete>;