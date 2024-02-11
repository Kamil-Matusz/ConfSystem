using ConfSystem.Modules.Users.Core.DTO;
using FluentValidation;

namespace ConfSystem.Modules.Users.Core.Validators;

public class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator()
    {
        RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password is required");
    }
}