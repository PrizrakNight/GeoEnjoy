using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Contracts.Requests;

public enum ReviewSorting
{
    None,
    Relevant,
    Newer,
    Older
}

public class GetReviewsRequest
{
    public ReviewSorting Sorting { get; set; } = ReviewSorting.Relevant;

    public PaginationDto Pagination { get; set; } = new();
}
