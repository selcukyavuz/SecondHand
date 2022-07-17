namespace SecondHand.Library.Commands.Ad;

using SecondHand.Models.Advertisement;
using MediatR;

public record InsertAdCommand(Ad Ad) : IRequest<Ad>;