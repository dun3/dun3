using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.Querycache
{
    public static class Queryable
    {
        public static IQueryable<T> AsCachedQuery<T>(this IQueryable<T> source)
        {
            var e = source.Expression as MethodCallExpression;

            //var l = Expression.Lambda<Func<IQueryable<T>, IQueryable<T>>>(e, e.Arguments.OfType<ParameterExpression>().ToArray());

            //return source;

            //var c = l.Compile();

            return new CachedQuery<T>(source.Expression);

            //return source.Provider.CreateQuery<T>(

            //throw new NotImplementedException();
        }
    }
}
