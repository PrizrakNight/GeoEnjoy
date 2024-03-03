using GeoEnjoy.Application.Dto;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IUserSocialActivityRepository : IRepository<UserSocialActivity>
{
    ValueTask<LikesDislikesDto> CountLikesDislikesAsync(Guid entityId, Guid userId,
        CancellationToken cancellationToken = default);
}
