using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.FilterByExample
{
    public static class PropertyInfoFilter
    {
        public static PropertyInfo[] FindRelevantPropertyz<T>(Expression<Func<T, object>>[] propertiesToExclude)
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

            return relevantPropertyz;
        }
    }
}
