using System.Linq.Expressions;

namespace GeoEnjoy.Application.Repositories
{
    public interface IEntitySortings<T>
    {
        EntitySorting<T>? SortingBy {  get; }
        EntitySorting<T>? SortingThenBy { get; }

        IEntitySortings<T> OrderBy(Expression<Func<T, object>> propertySelector);
        IEntitySortings<T> ThenBy(Expression<Func<T, object>> propertySelector);

        IEntitySortings<T> OrderByDescending(Expression<Func<T, object>> propertySelector);
        IEntitySortings<T> ThenByDescending(Expression<Func<T, object>> propertySelector);
    }
}
