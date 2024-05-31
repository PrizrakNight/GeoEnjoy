namespace GeoEnjoy.Application.Permissions;

public static class PointOfInterestPermissions
{
    // Write
    public static readonly string Add = "point-of-interest.add";
    public static readonly string Delete = "point-of-interest.delete";
    public static readonly string Update = "point-of-interest.update";

    // Read
    public static readonly string GetOwn = "point-of-interest.get.own";
    public static readonly string GetInRadius = "point-of-interest.get.in-radius";
}
