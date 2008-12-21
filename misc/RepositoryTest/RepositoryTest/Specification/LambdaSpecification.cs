using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RepositoryTest.Specification
{
    public class LambdaSpecification<T> : SpecificationBase<T>, ILambdaSpecification<T>
    {
        public LambdaSpecification(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException("predicate"); }
            m_predicate = predicate;
        }

        private readonly Expression<Func<T, bool>> m_predicate;
        public Expression<Func<T, bool>> Predicate
        {
            get { return m_predicate; }
        }

        private Func<T, bool> m_function;
        public override bool IsSatisfiedBy(T entity)
        {
            if (m_function == null)
            {
                m_function = m_predicate.Compile();
            }
            return m_function.Invoke(entity);
        }

        public ILambdaSpecification<T> And(ILambdaSpecification<T> other)
        {
            return And(other.Predicate);
        }

        public ILambdaSpecification<T> And(Expression<Func<T, bool>> other)
        {
            InvocationExpression otherInvoke = Expression.Invoke(other, Predicate.Parameters.Cast<Expression>());
            BinaryExpression andExpression = Expression.AndAlso(Predicate.Body, otherInvoke);

            return new LambdaSpecification<T>(Expression.Lambda<Func<T, bool>>(andExpression, Predicate.Parameters));
        }

        public ILambdaSpecification<T> Or(ILambdaSpecification<T> other)
        {
            return Or(other.Predicate);
        }
        public ILambdaSpecification<T> Or(Expression<Func<T, bool>> other)
        {
            InvocationExpression otherInvoke = Expression.Invoke(other, Predicate.Parameters.Cast<Expression>());
            BinaryExpression orExpression = Expression.OrElse(Predicate.Body, otherInvoke);

            return new LambdaSpecification<T>(Expression.Lambda<Func<T, bool>>(orExpression, Predicate.Parameters));
        }

        public new ILambdaSpecification<T> Not()
        {
            InvocationExpression otherInvoke = Expression.Invoke(Predicate, Predicate.Parameters.Cast<Expression>());
            UnaryExpression notExpression = Expression.Not(otherInvoke);

            return new LambdaSpecification<T>(Expression.Lambda<Func<T, bool>>(notExpression, Predicate.Parameters));
        }

        private static AllSpecification m_all = new AllSpecification();
        public static ILambdaSpecification<T> ALL
        {
            get
            {
                return m_all;
            }
        }

        private class AllSpecification : LambdaSpecification<T>
        {
            public AllSpecification()
                : base(t => true)
            {
            }
        }
    }
}
