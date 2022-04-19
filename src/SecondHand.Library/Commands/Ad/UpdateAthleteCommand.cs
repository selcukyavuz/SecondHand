namespace SecondHand.Library.Commands.Ad;

using SecondHand.Models.Adversitement;
using MediatR;

public record UpdateAdCommand(Ad ad) : IRequest<Ad>;