using ConfSystem.Modules.Speakers.Core.DTO;
using FluentValidation;

namespace ConfSystem.Modules.Speakers.Core.Validators;

public class SpeakerDtoValidator : AbstractValidator<SpeakerDto>
{
    public SpeakerDtoValidator()
    {
        RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(dto => dto.FullName).NotEmpty().WithMessage("Fullname is required");
        RuleFor(dto => dto.FullName).MinimumLength(3).MaximumLength(100);
        RuleFor(dto => dto.Bio).NotEmpty().WithMessage("Bio is required");
        RuleFor(dto => dto.Bio).MinimumLength(3).MaximumLength(100);
    }
}