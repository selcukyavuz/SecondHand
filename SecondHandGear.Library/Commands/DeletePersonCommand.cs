namespace SecondHandGear.Library.Commands;

using SecondHandGear.Library.Models;
using MediatR;

public record DeletePersonCommand(int id) : IRequest<bool>;