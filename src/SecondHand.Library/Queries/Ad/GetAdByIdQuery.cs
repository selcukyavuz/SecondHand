namespace SecondHand.Library.Queries.Ad;

using SecondHand.Models.Advertisement;
using MediatR;

public record GetAdByIdQuery(int Id) : IRequest<Ad>;