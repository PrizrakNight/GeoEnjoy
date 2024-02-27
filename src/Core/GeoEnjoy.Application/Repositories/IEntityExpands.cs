using System.Linq.Expressions;

namespace GeoEnjoy.Application.Repositories;

public interface IEntityExpands<T>
{
    IReadOnlySet<string> PropertiesToExpands { get; }

    IEntityExpands<T> Expand(Expression<Func<T, object>> propertySelector);
}
