using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PreOrderPlatform.Entity.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<T> LoadRelatedEntities<T>(this IQueryable<T> query, DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var visitedTypes = new HashSet<IEntityType>();
            return query.IncludeRelatedEntities(entityType, visitedTypes);
        }

        private static IQueryable<T> IncludeRelatedEntities<T>(
            this IQueryable<T> query,
            IEntityType entityType,
            HashSet<IEntityType> visitedTypes)
            where T : class
        {
            visitedTypes.Add(entityType);

            foreach (var navigation in entityType.GetNavigations())
            {
                var targetType = navigation.GetTargetType();

                if (visitedTypes.Contains(targetType))
                {
                    continue;
                }

                query = query.Include(navigation.Name);
                query = query.IncludeRelatedEntities(targetType, visitedTypes);
            }

            return query;
        }
    }
}