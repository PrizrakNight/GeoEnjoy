namespace GeoEnjoy.Domain.Entities;

public interface IModifiable
{
    DateTime? Updated { get; set; }
}
