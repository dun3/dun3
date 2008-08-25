using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.Automap.Reflection
{
    public class SingleProperty : Accessor
    {
        private readonly PropertyInfo _property;

        public SingleProperty(PropertyInfo property)
        {
            _property = property;
        }

        #region Accessor Members

        public string FieldName
        {
            get { return _property.Name; }
        }

        public Type PropertyType
        {
            get { return _property.PropertyType; }
        }

        public PropertyInfo InnerProperty
        {
            get { return _property; }
        }

        public Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression)
        {
            PropertyInfo property = ReflectionHelper.GetProperty(expression);
            return new PropertyChain(new[] { _property, property });
        }

        //public NotificationMessage[] Validate(object target)
        //{
        //    if (target == null)
        //    {
        //        return new NotificationMessage[0];
        //    }
        //    return Validator.ValidateField(target, _property.Name);
        //}

        public void SetValue(object target, object propertyValue)
        {
            if (_property.CanWrite)
            {
                _property.SetValue(target, propertyValue, null);
            }
        }

        public object GetValue(object target)
        {
            return _property.GetValue(target, null);
        }

        #endregion

        public static SingleProperty Build<T>(Expression<Func<T, object>> expression)
        {
            PropertyInfo property = ReflectionHelper.GetProperty(expression);
            return new SingleProperty(property);
        }

        public static SingleProperty Build<T>(string propertyName)
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            return new SingleProperty(property);
        }
    }
}
