namespace SecondHand.Library.Queries.Ad;

using SecondHand.Models.Advertisement;
using MediatR;

public record GetAdListQuery : IRequest<List<Ad>>;