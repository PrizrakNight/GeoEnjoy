namespace GeoEnjoy.Application.Dto
{
    public class PaginationDto
    {
        public const int DEFAULT_PAGE_SIZE = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
    }
}
