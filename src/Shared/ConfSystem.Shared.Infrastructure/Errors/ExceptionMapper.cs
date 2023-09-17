using System.Collections.Concurrent;
using System.Net;
using ConfSystem.Shared.Abstractions.Exceptions;
using Humanizer;

namespace ConfSystem.Shared.Infrastructure.Errors;

internal class ExceptionMapper : IExceptionMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new();

    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            CustomException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErorCode(ex), ex.Message)),
                HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error")),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Code, string Message);

    private record ErrorsResponse(params Error[] Errors);

    private static string GetErorCode(object exception)
    {
        var type = exception.GetType();
        return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
    }
}