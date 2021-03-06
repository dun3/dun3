﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.FilterByExample
{
    public static class Enumerable
    {
        public static IEnumerable<T> FilterByExample<T>(this IEnumerable<T> source, T example, params Expression<Func<T, object>>[] propertiesToExclude)
        {
            if (source == null) { throw new ArgumentNullException("source", "source is null."); }
            if (example == null) { throw new ArgumentNullException("example", "example is null."); }
            if (propertiesToExclude == null) { throw new ArgumentNullException("propertiesToExclude", "propertiesToExclude is null."); }
            if (propertiesToExclude.Contains(null)) { throw new ArgumentException("sequence contains a null item", "propertiesToExclude"); }

            var relevantPropertyz = PropertyInfoFilter.FindRelevantPropertyz<T>(propertiesToExclude);

            return FilterByExample<T>(source, example, relevantPropertyz);
        }

        private static IEnumerable<T> FilterByExample<T>(this IEnumerable<T> source, T example, params PropertyInfo[] relevantPropertyz)
        {
            // since this method is private no additional precondition check.

            Func<T, T, bool> filter = CreateFilter<T>(relevantPropertyz);

            foreach (T item in source)
            {
                if (filter(example, item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> FilterByExample<T>(this IEnumerable<T> source, T example, Func<T, T, bool> precompiledFilter)
        {
            if (source == null) { throw new ArgumentNullException("source", "source is null."); }
            if (example == null) { throw new ArgumentNullException("example", "example is null."); }
            if (precompiledFilter == null) { throw new ArgumentNullException("precompiledFilter", "precompiledFilter is null."); }

            foreach (T item in source)
            {
                if (precompiledFilter(example, item))
                {
                    yield return item;
                }
            }
        }

        public static Func<T, T, bool> CreateFilter<T>(params Expression<Func<T, object>>[] propertiesToExclude)
        {
            if (propertiesToExclude == null) { throw new ArgumentNullException("propertiesToExclude", "propertiesToExclude is null."); }
            if (propertiesToExclude.Contains(null)) { throw new ArgumentException("sequence contains a null item", "propertiesToExclude"); }

            var relevantPropertyz = PropertyInfoFilter.FindRelevantPropertyz<T>(propertiesToExclude);

            return CreateFilter<T>(relevantPropertyz);
        }

        private static Func<T, T, bool> CreateFilter<T>(PropertyInfo[] relevantPropertyz)
        {
            var parameterExample = Expression.Parameter(typeof(T), "example");
            var parameterOther = Expression.Parameter(typeof(T), "other");

            if (relevantPropertyz.Length == 0)
            {
                return (x, y) => true;
            }

            var expression = Expression.Equal(
                Expression.MakeMemberAccess(parameterOther, relevantPropertyz[0]),
                Expression.MakeMemberAccess(parameterExample, relevantPropertyz[0])
                );

            for (int i = 1; i < relevantPropertyz.Length; i++)
            {
                var next = Expression.Equal(
                    Expression.MakeMemberAccess(parameterOther, relevantPropertyz[i]),
                    Expression.MakeMemberAccess(parameterExample, relevantPropertyz[i])
                    );

                expression = Expression.AndAlso(expression, next);
            }

            var filter = Expression.Lambda<Func<T, T, bool>>(expression, parameterExample, parameterOther);

            return filter.Compile();
        }
    }
}
