namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record DeleteDetailedAthleteCommand(Guid id) : IRequest<bool>;