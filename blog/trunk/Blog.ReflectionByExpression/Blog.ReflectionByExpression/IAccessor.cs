
namespace Blog.ReflectionByExpression
{
    public interface IAccessor
    {
        object GetValue(object target);
        void SetValue(object target, object value);
    }
    public interface IAccessor<TValue>
    {
        TValue GetValue(object target);
        void SetValue(object target, TValue value);
    }
    public interface IAccessor<TTarget, TValue>
    {
        TValue GetValue(TTarget target);
        void SetValue(TTarget target, TValue value);
    }
}
