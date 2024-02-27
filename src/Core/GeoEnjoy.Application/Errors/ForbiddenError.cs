using FluentResults;

namespace GeoEnjoy.Application.Errors;

public class ForbiddenError : Error
{
    public ForbiddenError(string? message = null)
    {
        Message = message;
    }
}
