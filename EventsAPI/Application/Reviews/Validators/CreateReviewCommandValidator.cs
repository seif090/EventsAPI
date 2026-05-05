using EventsAPI.Application.Reviews.Commands;
using FluentValidation;

namespace EventsAPI.Application.Reviews.Validators;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.PhotographerId).NotEmpty();
        RuleFor(command => command.ClientId).NotEmpty();
        RuleFor(command => command.Rating).InclusiveBetween(1, 5);
        RuleFor(command => command.Comment).MaximumLength(1000);
    }
}
