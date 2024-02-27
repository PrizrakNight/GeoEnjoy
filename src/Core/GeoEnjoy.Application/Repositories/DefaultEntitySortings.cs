using System.Linq.Expressions;

namespace GeoEnjoy.Application.Repositories
{
    public class DefaultEntitySortings<T> : IEntitySortings<T>
    {
        public EntitySorting<T>? SortingBy { get; private set; }
        public EntitySorting<T>? SortingThenBy { get; private set; }

        public IEntitySortings<T> OrderBy(Expression<Func<T, object>> propertySelector)
        {
            SortingBy = new EntitySorting<T>(propertySelector, false);

            return this;
        }

        public IEntitySortings<T> OrderByDescending(Expression<Func<T, object>> propertySelector)
        {
            SortingBy = new EntitySorting<T>(propertySelector, true);

            return this;
        }

        public IEntitySortings<T> ThenBy(Expression<Func<T, object>> propertySelector)
        {
            if (SortingBy == null)
                throw new InvalidOperationException($"You can't use '{nameof(ThenBy)}' without setting up '{nameof(OrderBy)}' or '{nameof(OrderByDescending)}'");

            SortingThenBy = new EntitySorting<T>(propertySelector, false);

            return this;
        }

        public IEntitySortings<T> ThenByDescending(Expression<Func<T, object>> propertySelector)
        {
            if (SortingBy == null)
                throw new InvalidOperationException($"You can't use '{nameof(ThenByDescending)}' without setting up '{nameof(OrderBy)}' or '{nameof(OrderByDescending)}'");

            SortingThenBy = new EntitySorting<T>(propertySelector, true);

            return this;
        }
    }
}
