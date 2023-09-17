using ConfSystem.Shared.Abstractions.Exceptions;

namespace ConfSystem.Shared.Infrastructure.Errors;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}