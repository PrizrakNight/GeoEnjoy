namespace GeoEnjoy.Domain.Entities;

public interface ISocialActivityEntity : IDomainEntity
{
    ICollection<SocialActivity>? SocialActivities { get; set; }
}
