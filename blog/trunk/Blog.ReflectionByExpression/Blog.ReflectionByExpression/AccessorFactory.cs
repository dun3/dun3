using System;
using Blog.ReflectionByExpression.A_BasicReflection;
using Blog.ReflectionByExpression.B_EmitReflection;
using Blog.ReflectionByExpression.C_ExpressionReflection;

namespace Blog.ReflectionByExpression
{
    public class AccessorFactory
    {
        public static IAccessor CreateBasicAccessor(Type targetType, string propertyName)
        {
            return new BasicAccessor(targetType, propertyName);
        }

        public static IAccessor<TValue> CreateBasicAccessor<TValue>(Type targetType, string propertyName)
        {
            return new BasicAccessor<TValue>(targetType, propertyName);
        }

        public static IAccessor<TTarget, TValue> CreateBasicAccessor<TTarget, TValue>(string propertyName)
        {
            return new BasicAccessor<TTarget, TValue>(propertyName);
        }

        public static IAccessor CreateEmitAccessor(Type targetType, string propertyName)
        {
            return new EmitAccessor(targetType, propertyName);
        }

        public static IAccessor<TValue> CreateEmitAccessor<TValue>(Type targetType, string propertyName)
        {
            return new EmitAccessor<TValue>(targetType, propertyName);
        }

        public static IAccessor<TTarget, TValue> CreateEmitAccessor<TTarget, TValue>(string propertyName)
        {
            return new EmitAccessor<TTarget, TValue>(propertyName);
        }

        //public static IAccessor CreateExpressionAccessor(Type targetType, string propertyName)
        //{
        //    return new ExpressionAccessor(targetType, propertyName);
        //}

        //public static IAccessor<TValue> CreateExpressionAccessor<TValue>(Type targetType, string propertyName)
        //{
        //    return new ExpressionAccessor<TValue>(targetType, propertyName);
        //}

        public static IAccessor<TTarget, TValue> CreateExpressionAccessor<TTarget, TValue>(string propertyName)
        {
            return new ExpressionAccessor<TTarget, TValue>(propertyName);
        }
    }
}
