namespace SecondHand.Library.Commands.Ad;

using SecondHand.Models.Advertisement;
using MediatR;

public record UpdateAdCommand(Ad Ad) : IRequest<Ad>;