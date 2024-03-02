namespace GeoEnjoy.Application.Contracts.Response;

public class GeoUserResponse
{
    public Guid Id { get; set; }

    public string? AvatarUrl { get; set; }

    public string UserName { get; set; } = null!;
}
