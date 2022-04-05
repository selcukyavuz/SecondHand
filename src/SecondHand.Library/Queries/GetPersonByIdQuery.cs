namespace SecondHand.Library.Queries;

using SecondHand.Library.Models;
using MediatR;

public record GetPersonByIdQuery(Guid id) : IRequest<PersonModel>;