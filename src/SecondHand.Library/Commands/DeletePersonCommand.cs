namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record DeletePersonCommand(int id) : IRequest<bool>;