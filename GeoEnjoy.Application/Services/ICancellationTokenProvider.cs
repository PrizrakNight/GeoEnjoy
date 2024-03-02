namespace GeoEnjoy.Application.Services;

public interface ICancellationTokenProvider
{
    CancellationToken CancellationToken { get; }
}
