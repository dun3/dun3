using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Com.Hertkorn.Framework.FilterByExample
{
    public static class EnumerableAnonymous
    {
        public static IEnumerable<T> FilterByExample<T>(this IEnumerable<T> source, object example)
        {
            Func<object, T, bool> filter = CreateFilter<T>(example);

            foreach (T item in source)
            {
                if (filter(example, item))
                {
                    yield return item;
                }
            }
        }

        private static Func<object, T, bool> CreateFilter<T>(object example)
        {
            var examplePropertyz = example.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            if (examplePropertyz.Length == 0)
            {
                return (x, y) => true;
            }

            var otherPropertyz = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            var parameterExample = Expression.Parameter(typeof(object), "example");
            var parameterOther = Expression.Parameter(typeof(T), "other");

            var otherProperty = GetFittingProperty(examplePropertyz[0], otherPropertyz);

            var castExpression = Expression.Convert(parameterExample, example.GetType());

            var expression = Expression.Equal(
                Expression.MakeMemberAccess(parameterOther, otherProperty),
                Expression.MakeMemberAccess(castExpression, examplePropertyz[0])
                );

            for (int i = 1; i < examplePropertyz.Length; i++)
            {
                var otherPropertyInner = GetFittingProperty(examplePropertyz[i], otherPropertyz);

                var next = Expression.Equal(
                    Expression.MakeMemberAccess(parameterOther, otherPropertyInner),
                    Expression.MakeMemberAccess(castExpression, examplePropertyz[i])
                    );

                expression = Expression.AndAlso(expression, next);
            }

            var filter = Expression.Lambda<Func<object, T, bool>>(expression, parameterExample, parameterOther);

            return filter.Compile();
        }

        private static PropertyInfo GetFittingProperty(PropertyInfo exampleProperty, PropertyInfo[] otherPropertyz)
        {
            var filteredPropertyz = (from s in otherPropertyz
                                     where s.Name == exampleProperty.Name
                                     select s).ToList();

            if (filteredPropertyz.Count != 1)
            {
                throw new ArgumentException();
            }

            return filteredPropertyz[0];
        }
    }
}
