namespace GeoEnjoy.Application.Services;

public interface IMappingService
{
    TDestination Map<TDestination>(object source);

    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
}
