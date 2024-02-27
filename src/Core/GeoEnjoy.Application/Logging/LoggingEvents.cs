using Microsoft.Extensions.Logging;

namespace GeoEnjoy.Application.Logging;

public static class LoggingEvents
{
    public static EventId BlobObjectNotFound => new
    (
        id: 1000,
        name: nameof(BlobObjectNotFound)
    );
}
