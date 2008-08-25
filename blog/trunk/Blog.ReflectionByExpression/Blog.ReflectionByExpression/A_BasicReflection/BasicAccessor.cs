using System;
using System.Reflection;

namespace Blog.ReflectionByExpression.A_BasicReflection
{
    internal class BasicAccessor : IAccessor
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public BasicAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
            TargetProperty = TargetType.GetProperty(PropertyName);
        }

        #region IAccessor Members

        public object GetValue(object target)
        {
            return TargetProperty.GetValue(target, null);
        }

        public void SetValue(object target, object value)
        {
            TargetProperty.SetValue(target, value, null);
        }

        #endregion
    }

    internal class BasicAccessor<TValue> : IAccessor<TValue>
    {
        public string PropertyName { get; private set; }
        public Type TargetType { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public BasicAccessor(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
            TargetProperty = TargetType.GetProperty(PropertyName);
        }

        #region IAccessor<TValue> Members

        public TValue GetValue(object target)
        {
            return (TValue)TargetProperty.GetValue(target, null);
        }

        public void SetValue(object target, TValue value)
        {
            TargetProperty.SetValue(target, value, null);
        }

        #endregion
    }

    internal class BasicAccessor<TTarget, TValue> : IAccessor<TTarget, TValue>
    {
        public string PropertyName { get; private set; }
        public static Type TargetType { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        static BasicAccessor()
        {
            TargetType = typeof(TTarget);
        }

        public BasicAccessor(string propertyName)
        {
            PropertyName = propertyName;
            TargetProperty = TargetType.GetProperty(PropertyName);
        }

        #region IAccessor<TTarget,TValue> Members

        public TValue GetValue(TTarget target)
        {
            return (TValue)TargetProperty.GetValue(target, null);
        }

        public void SetValue(TTarget target, TValue value)
        {
            TargetProperty.SetValue(target, value, null);
        }

        #endregion
    }
}
