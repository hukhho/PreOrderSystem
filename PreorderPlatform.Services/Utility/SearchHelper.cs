using System;
using System.Linq;
using System.Linq.Expressions;

namespace PreorderPlatform.Service.Utility
{
    public static class SearchHelper
    {
        public static IQueryable<TEntity> GetWithSearch<TEntity>(
            this IQueryable<TEntity> query,
            object searchModel
        )
            where TEntity : class
        {
            foreach (var prop in searchModel.GetType().GetProperties())
            {
                var value = prop.GetValue(searchModel, null);

                if (value == null)
                    continue;

                var entityProperty = typeof(TEntity).GetProperty(prop.Name);

                if (entityProperty == null)
                    continue;

                // Build expression tree
                //--entity
                var param = Expression.Parameter(typeof(TEntity), "entity");
                //--entity.{PropertyName}
                var entityProp = Expression.Property(param, entityProperty.Name);

                Expression body;

                // Filter by string
                if (value is string stringValue && stringValue.Length > 0)
                {
                    //--searchValue
                    var searchValue = Expression.Constant(value);
                    //--entity.{PropertyName}.Contains(searchValue)
                    body = Expression.Call(entityProp, nameof(string.Contains), null, searchValue);
                }
                // Filter by int, Guid, DateTime, byte, and bool
                else if (
                      value is int
                      || value is Guid
                      || value is DateTime
                      || value is byte
                      || value is bool
                      || value is System.Enum   // Add this line
                  )
                {
                    // Check if the property type is nullable and convert the search value accordingly
                    if (Nullable.GetUnderlyingType(entityProp.Type) != null)
                    {
                        var searchValue = Expression.Constant(value, entityProp.Type);
                        //--entity.{PropertyName}.Equals(searchValue)
                        body = Expression.Equal(entityProp, searchValue);
                    }
                    else
                    {
                        var searchValue = Expression.Constant(value);
                        //--entity.{PropertyName}.Equals(searchValue)
                        body = Expression.Equal(entityProp, searchValue);
                    }
                }
                else
                {
                    continue;
                }



                //--entity => entity.{PropertyName}.(Contains/Equals)(searchValue)
                var exp = Expression.Lambda<Func<TEntity, bool>>(body, param);
                //entity.{PropertyName}.(Contains/Equals)(searchValue)
                query = query.Where(exp);
            }

            return query;
        }

        public static IQueryable<TEntity> FilterByDateInRange<TEntity>(
            this IQueryable<TEntity> query,
            DateTime? date,
            Expression<Func<TEntity, DateTime?>> startSelector,
            Expression<Func<TEntity, DateTime?>> endSelector
        )
        {
            if (date.HasValue)
            {
                var entityType = typeof(TEntity);
                var entityParameter = Expression.Parameter(entityType, "e");

                var startSelectorBody = ReplaceParameter(
                    startSelector.Body,
                    startSelector.Parameters[0],
                    entityParameter
                );
                var startCondition = Expression.LessThanOrEqual(
                    startSelectorBody,
                    Expression.Constant(date.Value, typeof(DateTime?))
                );

                var endSelectorBody = ReplaceParameter(
                    endSelector.Body,
                    endSelector.Parameters[0],
                    entityParameter
                );
                var endCondition = Expression.GreaterThanOrEqual(
                    endSelectorBody,
                    Expression.Constant(date.Value, typeof(DateTime?))
                );

                var combinedCondition = Expression.AndAlso(startCondition, endCondition);
                var lambda = Expression.Lambda<Func<TEntity, bool>>(
                    combinedCondition,
                    entityParameter
                );

                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<TEntity> FilterOrderByDate<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, DateTime?>> date,
            DateTime? startDate,
            DateTime? endDate
        )
        {
            if (date != null && startDate.HasValue && endDate.HasValue)
            {
                var entityType = typeof(TEntity);
                var entityParameter = Expression.Parameter(entityType, "e");

                var dateBody = ReplaceParameter(date.Body, date.Parameters[0], entityParameter);
                var dateCondition = Expression.AndAlso(
                    Expression.GreaterThanOrEqual(
                        dateBody,
                        Expression.Constant(startDate.Value, typeof(DateTime?))
                    ),
                    Expression.LessThanOrEqual(
                        dateBody,
                        Expression.Constant(endDate.Value, typeof(DateTime?))
                    )
                );

                var lambda = Expression.Lambda<Func<TEntity, bool>>(dateCondition, entityParameter);

                query = query.Where(lambda);
            }

            return query;
        }

        private static Expression ReplaceParameter(
            Expression expression,
            ParameterExpression oldParameter,
            ParameterExpression newParameter
        )
        {
            return new ParameterReplacer(oldParameter, newParameter).Visit(expression);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ParameterReplacer(
                ParameterExpression oldParameter,
                ParameterExpression newParameter
            )
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}
