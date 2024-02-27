using GeoEnjoy.Application.Services;
using Mapster;

namespace GeoEnjoy.Mapping.Mapster
{
    public class MapsterMappingService : IMappingService
    {
        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return source.Adapt(destination);
        }
    }
}
