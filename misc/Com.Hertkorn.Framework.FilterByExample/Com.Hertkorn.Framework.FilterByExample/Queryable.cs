using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.FilterByExample
{
    public static class Queryable
    {
        public static IQueryable<T> FilterByExample<T>(this IQueryable<T> source, T example, params Expression<Func<T, object>>[] propertiesToExclude)
        {
            if (source == null) { throw new ArgumentNullException("source", "source is null."); }
            if (example == null) { throw new ArgumentNullException("example", "example is null."); }
            if (propertiesToExclude == null) { throw new ArgumentNullException("propertiesToExclude", "propertiesToExclude is null."); }
            if (propertiesToExclude.Contains(null)) { throw new ArgumentException("sequence contains a null item", "propertiesToExclude"); }

            var relevantPropertyz = PropertyInfoFilter.FindRelevantPropertyz<T>(propertiesToExclude);

            return FilterByExample<T>(source, example, relevantPropertyz);
        }

        private static IQueryable<T> FilterByExample<T>(this IQueryable<T> source, T example, params PropertyInfo[] relevantPropertyz)
        {
            // since this method is private no additional precondition check.

            var exampleExpression = Expression.Constant(example, typeof(T));

            var otherParameterExpression = Expression.Parameter(typeof(T), "other");

            var filterExpression = Expression.Equal(
                Expression.MakeMemberAccess(otherParameterExpression, relevantPropertyz[0]),
                Expression.MakeMemberAccess(exampleExpression, relevantPropertyz[0])
                );

            for (int i = 1; i < relevantPropertyz.Length; i++)
            {
                var next = Expression.Equal(
                    Expression.MakeMemberAccess(otherParameterExpression, relevantPropertyz[i]),
                    Expression.MakeMemberAccess(exampleExpression, relevantPropertyz[i])
                    );

                filterExpression = Expression.AndAlso(filterExpression, next);
            }

            Expression<Func<T, bool>> filter = Expression.Lambda<Func<T, bool>>(filterExpression, otherParameterExpression);

            return source.Where(filter);
        }
    }
}
