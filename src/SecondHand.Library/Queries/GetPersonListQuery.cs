namespace SecondHand.Library.Queries;

using SecondHand.Library.Models;
using MediatR;

public record GetPersonListQuery : IRequest<List<PersonModel>>;