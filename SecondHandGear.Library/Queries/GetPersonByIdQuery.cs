namespace SecondHandGear.Library.Queries;

using SecondHandGear.Library.Models;
using MediatR;

public record GetPersonByIdQuery(int id) : IRequest<PersonModel>;