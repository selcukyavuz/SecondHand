namespace SecondHandGear.Library.Commands;

using SecondHandGear.Library.Models;
using MediatR;

public record InsertPersonCommand(string FirstName,string LastName) : IRequest<PersonModel>;