namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record UpdatePersonCommand(int id,string FirstName,string LastName) : IRequest<PersonModel>;