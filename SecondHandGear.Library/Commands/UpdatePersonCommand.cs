namespace SecondHandGear.Library.Commands;

using SecondHandGear.Library.Models;
using MediatR;

public record UpdatePersonCommand(int id,string FirstName,string LastName) : IRequest<PersonModel>;