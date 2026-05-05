using EventsAPI.Application.Orders.Commands;
using FluentValidation;

namespace EventsAPI.Application.Orders.Validators;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty();
        RuleFor(command => command.Status).IsInEnum();
    }
}
