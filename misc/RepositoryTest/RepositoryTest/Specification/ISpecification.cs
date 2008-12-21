using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RepositoryTest.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        ISpecification<T> And(ISpecification<T> rightHand);

        ISpecification<T> Or(ISpecification<T> rightHand);

        ISpecification<T> Not();
    }
}
