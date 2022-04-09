namespace SecondHand.Library.Commands.Athlete;

using MediatR;

public record DeleteAthleteCommand(int Id) : IRequest<bool>;