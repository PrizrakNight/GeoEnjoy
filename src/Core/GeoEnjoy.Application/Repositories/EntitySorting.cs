
using System.Linq.Expressions;

namespace GeoEnjoy.Application.Repositories
{
    public class EntitySorting<T>
    {
        public readonly Expression<Func<T, object>> PropertySelector;

        public readonly bool Descending;

        public EntitySorting(Expression<Func<T, object>> propertySelector, bool descending = false)
        {
            PropertySelector = propertySelector;
            Descending = descending;
        }
    }
}
