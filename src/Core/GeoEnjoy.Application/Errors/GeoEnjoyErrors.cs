using FluentResults;

namespace GeoEnjoy.Application.Errors;

public static class GeoEnjoyErrors
{
    public static IError EntityNotFound<T>()
    {
        return new NotFoundError($"{typeof(T).Name}NotFound");
    }

    public static IError PointOfInterestNotFound()
    {
        return new NotFoundError(nameof(PointOfInterestNotFound));
    }

    public static IError ReviewNotFound()
    {
        return new NotFoundError(nameof(ReviewNotFound));
    }

    public static IError FavoriteAlredyExists()
    {
        return new BadOperationError(nameof(FavoriteAlredyExists));
    }

    public static IError InvalidEntityType(string entityType)
    {
        return new BadOperationError($"Invalid entity type {entityType}");
    }

    public static IError ForbiddenDeleteNotOwnPointOfInterest()
    {
        return new ForbiddenError(nameof(ForbiddenDeleteNotOwnPointOfInterest));
    }

    public static IError ForbiddenUpdateNotOwnPointOfInterest()
    {
        return new ForbiddenError(nameof(ForbiddenUpdateNotOwnPointOfInterest));
    }

    public static IError ForbiddenLikeOrDislikeOwnReview()
    {
        return new ForbiddenError(nameof(ForbiddenLikeOrDislikeOwnReview));
    }
}
