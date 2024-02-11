using ConfSystem.Modules.Conferences.Core.DTO;
using FluentValidation;

namespace ConfSystem.Modules.Conferences.Core.Validators;

public class HostDtoValidator : AbstractValidator<HostDto>
{
    public HostDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Host Name is required");
        RuleFor(dto => dto.Name).MinimumLength(3).MaximumLength(100);
        RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(dto => dto.Description).MaximumLength(100);
    }
}