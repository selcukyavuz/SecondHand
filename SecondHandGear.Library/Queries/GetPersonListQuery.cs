namespace SecondHandGear.Library.Queries;

using SecondHandGear.Library.Models;
using MediatR;

public record GetPersonListQuery : IRequest<List<PersonModel>>;