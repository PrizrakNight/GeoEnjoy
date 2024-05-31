namespace GeoEnjoy.Application.Permissions;

public static class ReviewPermissions
{
    // Write
    public static readonly string Add = "review.add";
    public static readonly string Delete = "review.delete";

    // Read
    public static readonly string GetAll = "review.get.all";
    public static readonly string GetOwn = "review.get.own";
}
