using EventsAPI.Application.Shipping.Commands;
using FluentValidation;

namespace EventsAPI.Application.Shipping.Validators;

public class CreateShippingCommandValidator : AbstractValidator<CreateShippingCommand>
{
    public CreateShippingCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty();
        RuleFor(command => command.ProviderName).NotEmpty().MaximumLength(200);
        RuleFor(command => command.Cost).GreaterThanOrEqualTo(0);
        RuleFor(command => command.AddressLine1).NotEmpty().MaximumLength(300);
        RuleFor(command => command.City).NotEmpty().MaximumLength(100);
        RuleFor(command => command.Governorate).NotEmpty().MaximumLength(100);
        RuleFor(command => command.PostalCode).NotEmpty().MaximumLength(20);
    }
}
