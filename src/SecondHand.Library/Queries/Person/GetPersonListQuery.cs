namespace SecondHand.Library.Queries.Person;

using SecondHand.Library.Models;
using MediatR;

public record GetPersonListQuery : IRequest<List<PersonModel>>;