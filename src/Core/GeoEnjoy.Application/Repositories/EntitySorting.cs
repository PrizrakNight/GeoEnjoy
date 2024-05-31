
using System.Linq.Expressions;

namespace GeoEnjoy.Application.Repositories;

public class EntitySorting<T>(Expression<Func<T, object>> propertySelector, bool descending = false)
{
    public readonly Expression<Func<T, object>> PropertySelector = propertySelector;

    public readonly bool Descending = descending;
}
