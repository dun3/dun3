using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Blog.ReflectionByExpression.C_ExpressionReflection
{
    public static class ExpressionCompiler
    {
        public static Expression<Func<TTarget, TValue>> CreateGetter<TTarget, TValue>(string propertyName)
        {
            //ParameterExpression tValueGetter = Expression.Parameter(typeof(TTarget), "tValue");
            //UnaryExpression convertTarget = Expression.ConvertChecked(tValueGetter, typeof(TTarget));
            //MemberExpression property = Expression.Property(convertTarget, propertyName);
            //UnaryExpression convertProperty = Expression.Convert(property, typeof(TValue));

            ParameterExpression tValueGetter = Expression.Parameter(typeof(TTarget), "tValue");
            MemberExpression property = Expression.Property(tValueGetter, propertyName);

            return Expression.Lambda<Func<TTarget, TValue>>(property, tValueGetter);
        }

        public static Expression<Action<TTarget, TValue>> CreateSetter<TTarget, TValue>(string propertyName)
        {
            ParameterExpression tTargetSetter = Expression.Parameter(typeof(TTarget), "tTarget");
            ParameterExpression tValueSetter = Expression.Parameter(typeof(TValue), "tValue");
            MethodCallExpression propertyValue = Expression.Call(tTargetSetter, typeof(TTarget).GetProperty(propertyName).GetSetMethod(), tValueSetter);

            return Expression.Lambda<Action<TTarget, TValue>>(propertyValue, tTargetSetter, tValueSetter);
        }

        internal static Expression<Func<object, TValue>> CreateGetter<TValue>(Type targetType, string propertyName)
        {
            //ParameterExpression tValueGetter = Expression.Parameter(typeof(TTarget), "tValue");
            //UnaryExpression convertTarget = Expression.ConvertChecked(tValueGetter, typeof(TTarget));
            //MemberExpression property = Expression.Property(convertTarget, propertyName);
            //UnaryExpression convertProperty = Expression.Convert(property, typeof(TValue));

            ParameterExpression tValueGetter = Expression.Parameter(typeof(object), "tValue");
            UnaryExpression convertTarget = Expression.ConvertChecked(tValueGetter, targetType);
            MemberExpression property = Expression.Property(convertTarget, propertyName);

            return Expression.Lambda<Func<object, TValue>>(property, tValueGetter);
        }

        internal static Expression<Action<object, TValue>> CreateSetter<TValue>(Type targetType, string propertyName)
        {
            ParameterExpression tTargetSetter = Expression.Parameter(typeof(object), "tTarget");
            UnaryExpression convertTarget = Expression.ConvertChecked(tTargetSetter, targetType);
            ParameterExpression tValueSetter = Expression.Parameter(typeof(TValue), "tValue");
            MethodCallExpression propertyValue = Expression.Call(convertTarget, targetType.GetProperty(propertyName).GetSetMethod(), tValueSetter);

            return Expression.Lambda<Action<object, TValue>>(propertyValue, tTargetSetter, tValueSetter);
        }

        internal static Expression<Func<object, object>> CreateGetter(Type targetType, string propertyName)
        {
            ParameterExpression tValueGetter = Expression.Parameter(typeof(object), "tValue");
            UnaryExpression convertTarget = Expression.ConvertChecked(tValueGetter, targetType);
            MemberExpression property = Expression.Property(convertTarget, propertyName);
            UnaryExpression convertProperty = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<object, object>>(convertProperty, tValueGetter);
        }

        internal static Expression<Action<object, object>> CreateSetter(Type targetType, string propertyName)
        {
            ParameterExpression tTargetSetter = Expression.Parameter(typeof(object), "tTarget");
            UnaryExpression convertTarget = Expression.ConvertChecked(tTargetSetter, targetType);
            ParameterExpression tValueSetter = Expression.Parameter(typeof(object), "tValue");
            UnaryExpression convertValue = Expression.Convert(tValueSetter, targetType.GetProperty(propertyName).PropertyType);
            MethodCallExpression propertyValue = Expression.Call(convertTarget, targetType.GetProperty(propertyName).GetSetMethod(), convertValue);

            return Expression.Lambda<Action<object, object>>(propertyValue, tTargetSetter, tValueSetter);
        }
    }
}
