namespace SecondHand.Library.Commands.Person;

using MediatR;

public record DeletePersonCommand(Guid id) : IRequest<bool>;