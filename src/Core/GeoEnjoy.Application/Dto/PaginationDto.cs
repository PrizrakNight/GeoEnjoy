namespace GeoEnjoy.Application.Dto;

public class PaginationDto
{
    public const int DEFAULT_PAGE_SIZE = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = Math.Clamp(value, 1, int.MaxValue);
    }

    private int _pageNumber = 1;

    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
}
