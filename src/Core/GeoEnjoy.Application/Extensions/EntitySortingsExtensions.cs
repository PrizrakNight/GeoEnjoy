using GeoEnjoy.Application.Contracts.Requests;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Exceptions;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Extensions
{
    public static class EntitySortingsExtensions
    {
        public static void UseSortingWay<T>(this IEntitySortings<T> entitySortings,
            PointsOfInterestSorting sortingWay)
            where T : ICreatable
        {
            switch (sortingWay)
            {
                case PointsOfInterestSorting.None:
                    // We do not use sorting
                    break;
                case PointsOfInterestSorting.Newer:
                    entitySortings.OrderByDescending(x => x.Created);
                    break;
                case PointsOfInterestSorting.Older:
                    entitySortings.OrderBy(x => x.Created);
                    break;
                default:
                    throw new SortingNotImplementedException(sortingWay.ToString());
            }
        }

        public static void UseSortingWay(this IEntitySortings<Review> entitySortings,
            ReviewSorting sortingWay)
        {
            switch (sortingWay)
            {
                case ReviewSorting.None:
                    // We do not use sorting
                    break;
                case ReviewSorting.Relevant:
                    entitySortings
                        .OrderByDescending(x => x.SocialActivities!.Count)
                        .ThenByDescending(x => x.Created);
                    break;
                case ReviewSorting.Newer:
                    entitySortings.OrderByDescending(x => x.Created);
                    break;
                case ReviewSorting.Older:
                    entitySortings.OrderBy(x => x.Created);
                    break;
                default:
                    throw new SortingNotImplementedException(sortingWay.ToString());
            }
        }
    }
}
