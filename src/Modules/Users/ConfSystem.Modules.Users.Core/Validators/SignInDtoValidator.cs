using ConfSystem.Modules.Users.Core.DTO;
using FluentValidation;

namespace ConfSystem.Modules.Users.Core.Validators;

public class SignInDtoValidator : AbstractValidator<SignInDto>
{
    public SignInDtoValidator()
    {
        RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(dto => dto.Password).NotEmpty().WithMessage("Password is required");
    }
}