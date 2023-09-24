using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}