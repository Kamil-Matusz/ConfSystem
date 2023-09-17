namespace ConfSystem.Shared.Abstractions.Exceptions;

public interface IExceptionMapper
{
    ExceptionResponse Map(Exception exception);
}