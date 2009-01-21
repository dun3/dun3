using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Com.Hertkorn.Framework.PropertyWiring
{
    public class ReflectionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            Expression operand = UnboxIfNecessary(property.Body);

            if (operand.NodeType == ExpressionType.MemberAccess)
            {
                return (operand as MemberExpression).Member.Name;
            }
            else
            {
                throw new ArgumentException("Die Expression beinhaltet keinen Member", "property");
            }
        }

        private static Expression UnboxIfNecessary(Expression operand)
        {
            if (operand.NodeType == ExpressionType.Convert)
            {
                operand = (operand as UnaryExpression).Operand;
            }

            return operand;
        }

        [Obsolete("Please use GetPropertyName, since this method has proplems with value types")]
        public static string GetPropertyNameObsolete<T>(Expression<Func<T, object>> property)
        {
            MemberExpression me = (MemberExpression)property.Body;
            return me.Member.Name;
        }
    }
}
