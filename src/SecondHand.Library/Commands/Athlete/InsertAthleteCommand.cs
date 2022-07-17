namespace SecondHand.Library.Commands.Athlete;

using SecondHand.Models.Strava;
using MediatR;

public record InsertAthleteCommand(Athlete Athlete) : IRequest<Athlete>;