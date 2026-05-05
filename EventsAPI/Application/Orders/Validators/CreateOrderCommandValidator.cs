using EventsAPI.Application.Orders.Commands;
using FluentValidation;

namespace EventsAPI.Application.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.ClientId).NotEmpty();
        RuleFor(command => command.AlbumTypeId).NotEmpty();
        RuleFor(command => command.PhotoIds).NotEmpty();
    }
}
