namespace SecondHand.Library.Queries.Ad;

using SecondHand.Models.Adversitement;
using MediatR;

public record GetAdByIdQuery(int Id) : IRequest<Ad>;