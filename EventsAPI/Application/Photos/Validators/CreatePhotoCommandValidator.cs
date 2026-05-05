using EventsAPI.Application.Photos.Commands;
using FluentValidation;

namespace EventsAPI.Application.Photos.Validators;

public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
{
    public CreatePhotoCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.PhotographerId).NotEmpty();
        RuleFor(command => command.Url).NotEmpty().MaximumLength(500);
    }
}
