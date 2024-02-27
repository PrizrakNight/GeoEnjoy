using System.Collections.Frozen;
using System.Linq.Expressions;
using System.Reflection;

namespace GeoEnjoy.Application.Repositories;

public class DefaultEntityExpands<T> : IEntityExpands<T>
{
    #region NestedTypes

    private sealed class NavigationPropertyPathVisitor : ExpressionVisitor
    {
        public string Path
        {
            get
            {
                if (_visitedMembers == null || _visitedMembers.Count == 0)
                    return string.Empty;

                var pathElements = _visitedMembers
                    .Select(x => x.Name)
                    .Reverse()
                    .ToArray();

                return string.Join(".", pathElements);
            }
        }
        private readonly List<MemberInfo> _visitedMembers = new();

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member is PropertyInfo || node.Member is FieldInfo)
            {
                _visitedMembers.Add(node.Member);

                return base.VisitMember(node);
            }

            throw new ArgumentException("The path can only contain properties or fields", nameof(node));
        }
    }

    #endregion

    public IReadOnlySet<string> PropertiesToExpands => _propertiesToExpands.ToFrozenSet();

    private readonly HashSet<string> _propertiesToExpands = [];

    public IEntityExpands<T> Expand(Expression<Func<T, object>> propertySelector)
    {
        var visitor = new NavigationPropertyPathVisitor();

        visitor.Visit(propertySelector);

        _propertiesToExpands.Add(visitor.Path);

        return this;
    }
}
