using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;

public enum PointsOfInterestSorting
{
    None,
    Newer,
    Older
}

public class GetOwnPointsOfInterestRequest
{
    public PaginationDto Pagination { get; init; } = new();

    public PointsOfInterestSorting Sorting { get; set; } = PointsOfInterestSorting.Newer;
}
