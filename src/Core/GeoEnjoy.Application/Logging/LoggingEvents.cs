using Microsoft.Extensions.Logging;

namespace GeoEnjoy.Application.Logging;

public static class LoggingEvents
{
    public static EventId BlobObjectNotFound => new
    (
        id: 1000,
        name: nameof(BlobObjectNotFound)
    );

    public static EventId SocialActivitySet => new
    (
        id: 5000,
        name: nameof(SocialActivitySet)
    );

    public static EventId SocialActivityDeleted => new
    (
        id: 5001,
        name: nameof(SocialActivityDeleted)
    );
}
