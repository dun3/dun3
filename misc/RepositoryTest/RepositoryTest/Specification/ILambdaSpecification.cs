using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RepositoryTest.Specification
{
    public interface ILambdaSpecification<T> : ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }

        ILambdaSpecification<T> And(ILambdaSpecification<T> other);
        ILambdaSpecification<T> Or(ILambdaSpecification<T> other);
        ILambdaSpecification<T> And(Expression<Func<T, bool>> other);
        ILambdaSpecification<T> Or(Expression<Func<T, bool>> other);
        new ILambdaSpecification<T> Not();
    }
}
