using System;
using FastDynamicPropertyAccessor;

namespace Blog.ReflectionByExpression.B_EmitReflection
{
    internal class EmitAccessor : IAccessor
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public IPropertyAccessor TargetProperty { get; private set; }

        public EmitAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
            TargetProperty = new PropertyAccessor(TargetType, PropertyName);
        }

        #region IAccessor Members

        public object GetValue(object target)
        {
            return TargetProperty.Get(target);
        }

        public void SetValue(object target, object value)
        {
            TargetProperty.Set(target, value);
        }
    }

    internal class EmitAccessor<TValue> : IAccessor<TValue>
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public IPropertyAccessor TargetProperty { get; private set; }

        public EmitAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
            TargetProperty = new PropertyAccessor(TargetType, PropertyName);
        }

        public TValue GetValue(object target)
        {
            return (TValue)TargetProperty.Get(target);
        }

        public void SetValue(object target, TValue value)
        {
            TargetProperty.Set(target, value);
        }
    }

    internal class EmitAccessor<TTarget, TValue> : IAccessor<TTarget, TValue>
    {
        public string PropertyName { get; private set; }
        public static Type TargetType { get; private set; }
        public IGenericPropertyAccessor<TTarget, TValue> TargetProperty { get; private set; }

        static EmitAccessor()
        {
            TargetType = typeof(TTarget);
        }

        public EmitAccessor(string propertyName)
        {
            PropertyName = propertyName;
            TargetProperty = new GenericPropertyAccessor<TTarget, TValue>(PropertyName);
        }

        public TValue GetValue(TTarget target)
        {
            return TargetProperty.Get(target);
        }

        public void SetValue(TTarget target, TValue value)
        {
            TargetProperty.Set(target, value);
        }

        #endregion
    }
}
