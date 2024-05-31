namespace GeoEnjoy.Domain.Entities;

public class MediaFile : IDomainEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public long SizeInBytes { get; set; }
}
