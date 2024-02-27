using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Contracts.Request
{
    public enum SortingReviews
    {
        None,
        Relevant,
        Newer,
        Older
    }

    public class GetReviewsRequest
    {
        public SortingReviews Sorting { get; set; } = SortingReviews.Relevant;

        public PaginationDto Pagination { get; set; } = new();
    }
}
