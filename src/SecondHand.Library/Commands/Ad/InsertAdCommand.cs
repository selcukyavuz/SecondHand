namespace SecondHand.Library.Commands.Ad;

using SecondHand.Models.Adversitement;
using MediatR;

public record InsertAdCommand(Ad ad) : IRequest<Ad>;