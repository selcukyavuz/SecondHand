namespace SecondHand.Library.Commands.DetailedAthlete;

using MediatR;

public record DeleteDetailedAthleteCommand(int Id) : IRequest<bool>;