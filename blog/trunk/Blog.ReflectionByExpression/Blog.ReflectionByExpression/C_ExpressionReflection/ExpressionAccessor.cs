using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Blog.ReflectionByExpression.C_ExpressionReflection
{
    internal class ExpressionAccessor : IAccessor
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public Func<object, object> TargetPropertyGetter { get; private set; }
        public Action<object, object> TargetPropertySetter { get; private set; }

        public ExpressionAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;

            TargetPropertyGetter = ExpressionCompiler.CreateGetter(TargetType, PropertyName).Compile();
            TargetPropertySetter = ExpressionCompiler.CreateSetter(TargetType, PropertyName).Compile();
        }

        public object GetValue(object target)
        {
            return TargetPropertyGetter(target);
        }

        public void SetValue(object target, object value)
        {
            TargetPropertySetter(target, value);
        }
    }

    internal class ExpressionAccessor<TValue> : IAccessor<TValue>
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public Func<object, TValue> TargetPropertyGetter { get; private set; }
        public Action<object, TValue> TargetPropertySetter { get; private set; }

        public ExpressionAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;

            TargetPropertyGetter = ExpressionCompiler.CreateGetter<TValue>(TargetType, PropertyName).Compile();
            TargetPropertySetter = ExpressionCompiler.CreateSetter<TValue>(TargetType, PropertyName).Compile();
        }

        public TValue GetValue(object target)
        {
            return TargetPropertyGetter(target);
        }

        public void SetValue(object target, TValue value)
        {
            TargetPropertySetter(target, value);
        }
    }

    internal class ExpressionAccessor<TTarget, TValue> : IAccessor<TTarget, TValue>
    {
        public string PropertyName { get; private set; }
        public static Type TargetType { get; private set; }
        public Func<TTarget, TValue> TargetPropertyGetter { get; private set; }
        public Action<TTarget, TValue> TargetPropertySetter { get; private set; }

        static ExpressionAccessor()
        {
            TargetType = typeof(TTarget);
        }

        public ExpressionAccessor(string propertyName)
        {
            PropertyName = propertyName;

            TargetPropertyGetter = ExpressionCompiler.CreateGetter<TTarget, TValue>(PropertyName).Compile();
            TargetPropertySetter = ExpressionCompiler.CreateSetter<TTarget, TValue>(PropertyName).Compile();
        }

        public TValue GetValue(TTarget target)
        {
            return TargetPropertyGetter(target);
        }

        public void SetValue(TTarget target, TValue value)
        {
            TargetPropertySetter(target, value);
        }
    }
}
