using ConfSystem.Modules.Conferences.Core.DTO;
using FluentValidation;

namespace ConfSystem.Modules.Conferences.Core.Validators;

public class ConferenceDtoValidator : AbstractValidator<ConferenceDto>
{
    public ConferenceDtoValidator()
    {
        RuleFor(dto => dto.HostId).NotEmpty().WithMessage("Host id is required");
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Conference Name is required");
        RuleFor(dto => dto.Name).MinimumLength(3).MaximumLength(100);
    }
}