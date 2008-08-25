using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LogViewerTest.Framework.Reflection
{
    public interface Accessor
    {
        string FieldName { get; }

        Type PropertyType { get; }
        PropertyInfo InnerProperty { get; }
        void SetValue(object target, object propertyValue);
        object GetValue(object target);

        Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression);

        //NotificationMessage[] Validate(object target);
    }
}
