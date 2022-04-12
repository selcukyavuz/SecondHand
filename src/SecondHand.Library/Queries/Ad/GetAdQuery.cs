namespace SecondHand.Library.Queries.Ad;

using SecondHand.Models.Adversitement;
using MediatR;

public record GetAdListQuery : IRequest<List<Ad>>;