namespace SecondHand.Library.Commands.DetailedAthlete;

using MediatR;

public record DeleteDetailedAthleteCommand(long? id) : IRequest<bool>;