using FluentResults;

namespace GeoEnjoy.Application.Errors;

public class BadOperationError : Error
{
    public BadOperationError(string? message = null)
    {
        Message = message;
    }
}
