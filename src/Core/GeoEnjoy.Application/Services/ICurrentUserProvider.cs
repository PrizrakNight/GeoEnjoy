using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Services;

public interface ICurrentUserProvider
{
    Guid Id { get; }

    UserInfo Info { get; }
}
