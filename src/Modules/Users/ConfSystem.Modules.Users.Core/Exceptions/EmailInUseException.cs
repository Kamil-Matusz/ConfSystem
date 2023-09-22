using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Modules.Users.Core.Exceptions;

internal class EmailInUseException : CustomException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}