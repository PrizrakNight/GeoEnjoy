using System.Text.Json.Serialization;

namespace GeoEnjoy.WebApi.Contracts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EntityType
{
    Review,
    PointOfInterest
}
