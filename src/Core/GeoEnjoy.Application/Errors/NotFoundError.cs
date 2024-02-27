using FluentResults;

namespace GeoEnjoy.Application.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string? message = null)
    {
        Message = message;
    }
}
