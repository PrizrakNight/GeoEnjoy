using GeoEnjoy.Domain;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public class UserSocialActivitySpecifications
{
    public static Spec<UserSocialActivity> ByEntityId(Guid entityId)
    {
        return new Spec<UserSocialActivity>(x => x.Id == entityId);
    }

    public static Spec<UserSocialActivity> ByUserId(Guid userId)
    {
        return new Spec<UserSocialActivity>(x => x.UserId == userId);
    }
}
