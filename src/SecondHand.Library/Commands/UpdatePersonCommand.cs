namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record UpdatePersonCommand(Guid id,string FirstName,string LastName) : IRequest<PersonModel>;