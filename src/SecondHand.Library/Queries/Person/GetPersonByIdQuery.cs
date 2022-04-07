namespace SecondHand.Library.Queries.Person;

using SecondHand.Library.Models;
using MediatR;

public record GetPersonByIdQuery(Guid id) : IRequest<PersonModel>;