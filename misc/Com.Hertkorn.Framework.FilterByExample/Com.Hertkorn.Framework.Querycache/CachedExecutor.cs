using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Com.Hertkorn.Framework.Querycache
{
    internal abstract class CachedExecutor
    {
        // Methods
        protected CachedExecutor()
        {
        }

        internal static CachedExecutor Create(Expression expression)
        {
            return (CachedExecutor)Activator.CreateInstance(typeof(CachedExecutor<>).MakeGenericType(new Type[] { expression.Type }), BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new object[] { expression }, null);
        }

        internal abstract object ExecuteBoxed();
    }

    internal class CachedExecutor<T> : CachedExecutor
    {
        private Func<IQueryable<T>, IEnumerable<T>> m_baseExpression;

        // Fields
        private Expression expression;
        private Func<T> func;

        // Methods
        internal CachedExecutor(Expression expression)
        {
            this.expression = expression;
        }

        internal T Execute()
        {
            if (this.func == null)
            {
                Expression<Func<T>> lambda = Expression.Lambda<Func<T>>(this.expression, (IEnumerable<ParameterExpression>)null);
                this.func = lambda.Compile();
            }
            return this.func();
        }

        internal override object ExecuteBoxed()
        {
            return this.Execute();
        }
    }
}
