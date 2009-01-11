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

            var relevantPropertyz = FindRelevantPropertyz<T>(propertiesToExclude);



            return FilterByExample<T>(source, example, relevantPropertyz);
        }

        private static IQueryable<T> FilterByExample<T>(this IQueryable<T> source, T example, params PropertyInfo[] relevantPropertyz)
        {
            // since this method is private no additional precondition check.

            Func<T, IQueryable<T>, IQueryable<T>> t = CreateFuncNoCurry<T>(relevantPropertyz);

            return t(example, source);
        }

        private static PropertyInfo[] FindRelevantPropertyz<T>(Expression<Func<T, object>>[] propertiesToExclude)
        {
            // since this method is private no additional precondition check.

            // without boxing
            var withoutBoxing = from property in propertiesToExclude
                                where property.Body.NodeType == ExpressionType.MemberAccess
                                select (property.Body as MemberExpression).Member.Name;

            // with boxing (Properties that give access to a value type)
            var withBoxing = from property in propertiesToExclude
                             where property.Body.NodeType == ExpressionType.Convert
                             let convert = property.Body as UnaryExpression
                             where convert.Operand.NodeType == ExpressionType.MemberAccess
                             select (convert.Operand as MemberExpression).Member.Name;

            var allPropertyz = withoutBoxing.Union(withBoxing).ToArray();

#warning This is not enough for interface handling - FlattenHierarchy does not work as expected on interfaces
            var allKnownPropertyz = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            // Does not work propertly without the a.GetGetMethod().IsPublic, since 
            // a.CanRead will return true, even if
            // object Test { private get; set; } ...
            var relevantPropertyz = allKnownPropertyz.Where(
                (thisProperty) =>
                {
                    if (allPropertyz.Contains(thisProperty.Name))
                    {
                        // this is a property we are asked to ignore
                        return false;
                    }
                    else
                    {
                        MethodInfo getMethod = thisProperty.GetGetMethod();

                        // Test if the get is publically accessible
                        return getMethod != null && getMethod.IsPublic;
                    }
                }
                ).ToArray();

            if (relevantPropertyz.Length < 1) { throw new ArgumentException("All public properties are marked as 'ignore'. Nothing to filter by."); }

            return relevantPropertyz;
        }

        private static Func<T, IQueryable<T>, IQueryable<T>> CreateFuncNoCurry<T>(PropertyInfo[] relevantPropertyz)
        {
            PropertyInfo[] t = relevantPropertyz;

            var parameterExample = Expression.Parameter(typeof(T), "ex");
            var parameterA = Expression.Parameter(typeof(T), "a");

            var expression = Expression.Equal(
                Expression.MakeMemberAccess(parameterA, t[0]),
                Expression.MakeMemberAccess(parameterExample, t[0])
                );

            for (int i = 1; i < t.Length; i++)
            {
                var next = Expression.Equal(
                    Expression.MakeMemberAccess(parameterA, t[i]),
                    Expression.MakeMemberAccess(parameterExample, t[i])
                    );

                expression = Expression.AndAlso(expression, next);
            }

            var test = Expression.Lambda<Func<T, T, bool>>(expression, parameterExample, parameterA);

            var testCompile = test.Compile();

            //Func<T, IQueryable<T>, IQueryable<T>> intermediate = (ex, a) =>
            //    {
            //        List<T> temp = new List<T>();
            //        foreach (var item in a)
            //        {
            //            if (testCompile(item, ex))
            //            {
            //                temp.Add(item);
            //            }
            //        }

            //        return temp.AsQueryable();
            //    };

            //return intermediate;


            // WAAAAAAAAAAAAAAAAAAAAT, die ist um faktor 10000 oder so langsamer als obige
            //Func<T, IQueryable<T>, IQueryable<T>> intermediate = (ex, a) =>
            //    {
            //        var r = from m in a
            //                where testCompile(m, ex)
            //                select m;
            //        return r;
            //    };

            //return intermediate;

            Func<T, IQueryable<T>, IQueryable<T>> intermediate = (ex, a) =>
            {
                Expression<Func<T, bool>> pred = (inner) => testCompile(inner, ex);
                var t1 = a.Where(pred);

                var t2 = t1.Expression;

                //return t1;

                Func<T, bool> pred2 = (inner) => testCompile(inner, ex);

                IQueryable<T> aWhereAsQueryable = a.Where(pred2).AsQueryable();

                var t3 = aWhereAsQueryable.Expression;

                return aWhereAsQueryable;
            };

            return intermediate;
        }
    }
}
