using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

namespace Com.Hertkorn.Framework.Querycache
{
    internal abstract class CachedQuery
    {
        // Methods
        protected CachedQuery()
        {
        }

        internal static IQueryable Create(Type elementType, IEnumerable sequence)
        {
            return (IQueryable)Activator.CreateInstance(typeof(CachedQuery<>).MakeGenericType(new Type[] { elementType }), BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new object[] { sequence }, null);
        }

        internal static IQueryable Create(Type elementType, Expression expression)
        {
            return (IQueryable)Activator.CreateInstance(typeof(CachedQuery<>).MakeGenericType(new Type[] { elementType }), BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new object[] { expression }, null);
        }

        // Properties
        internal abstract IEnumerable Enumerable { get; }

        internal abstract Expression Expression { get; }
    }

    internal class CachedQuery<T> : CachedQuery, IOrderedQueryable<T>, IQueryable<T>, IOrderedQueryable, IQueryable, IQueryProvider, IEnumerable<T>, IEnumerable
    {
        private static Func<IQueryable<T>, IEnumerable<T>> m_baseExpression;
        // Fields
        private IEnumerable<T> m_enumerable;
        private Expression m_expression;

        // Methods
        internal CachedQuery(IEnumerable<T> enumerable)
        {
            m_enumerable = enumerable;
            m_expression = Expression.Constant(this);
        }

        internal CachedQuery(Expression expression)
        {
            m_expression = expression;

            if (m_baseExpression == null)
            {
                MethodCallExpression originalMethodCallExpression = expression as MethodCallExpression;

                ParameterExpression expressionParameter = Expression.Parameter(typeof(IQueryable<T>), "iqueryable");

                MethodCallExpression methodCall = Expression.Call(originalMethodCallExpression.Object, originalMethodCallExpression.Method, expressionParameter, originalMethodCallExpression.Arguments[1]);

                m_baseExpression = Expression.Lambda<Func<IQueryable<T>, IEnumerable<T>>>(methodCall, expressionParameter).Compile();
            }
        }

        private IEnumerator<T> GetEnumerator()
        {
            if (m_enumerable == null)
            {
                m_enumerable = m_baseExpression(((m_expression as MethodCallExpression).Arguments[0] as ConstantExpression).Value as IQueryable<T>);
            }

            return m_enumerable.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            if (expression == null)
            {
                throw Error.ArgumentNull("expression");
            }
            Type type = TypeHelper.FindGenericType(typeof(IQueryable<>), expression.Type);
            if (type == null)
            {
                throw Error.ArgumentNotValid("expression");
            }
            return CachedQuery.Create(type.GetGenericArguments()[0], expression);
        }

        IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
        {
            if (expression == null)
            {
                throw Error.ArgumentNull("expression");
            }
            if (!typeof(IQueryable<S>).IsAssignableFrom(expression.Type))
            {
                throw Error.ArgumentNotValid("expression");
            }
            return new CachedQuery<S>(expression);
        }

        S IQueryProvider.Execute<S>(Expression expression)
        {
            if (expression == null)
            {
                throw Error.ArgumentNull("expression");
            }
            if (!typeof(S).IsAssignableFrom(expression.Type))
            {
                throw Error.ArgumentNotValid("expression");
            }
            return new CachedExecutor<S>(expression).Execute();
        }

        object IQueryProvider.Execute(Expression expression)
        {
            if (expression == null)
            {
                throw Error.ArgumentNull("expression");
            }
            typeof(CachedExecutor<>).MakeGenericType(new Type[] { expression.Type });
            return CachedExecutor.Create(expression).ExecuteBoxed();
        }

        public override string ToString()
        {
            ConstantExpression expression = m_expression as ConstantExpression;
            if ((expression == null) || (expression.Value != this))
            {
                return m_expression.ToString();
            }
            if (m_enumerable != null)
            {
                return m_enumerable.ToString();
            }
            return "null";
        }

        // Properties
        internal override IEnumerable Enumerable
        {
            get
            {
                return m_enumerable;
            }
        }

        internal override Expression Expression
        {
            get
            {
                return m_expression;
            }
        }

        Type IQueryable.ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return m_expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return this;
            }
        }
    }


}
