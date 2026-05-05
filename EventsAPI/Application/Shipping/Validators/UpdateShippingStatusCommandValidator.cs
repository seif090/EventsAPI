using EventsAPI.Application.Shipping.Commands;
using FluentValidation;

namespace EventsAPI.Application.Shipping.Validators;

public class UpdateShippingStatusCommandValidator : AbstractValidator<UpdateShippingStatusCommand>
{
    public UpdateShippingStatusCommandValidator()
    {
        RuleFor(command => command.ShippingId).NotEmpty();
        RuleFor(command => command.Status).IsInEnum();
    }
}
