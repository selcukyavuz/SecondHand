namespace SecondHand.Library.Commands;

using SecondHand.Library.Models;
using MediatR;

public record InsertPersonCommand(string FirstName,string LastName) : IRequest<PersonModel>;