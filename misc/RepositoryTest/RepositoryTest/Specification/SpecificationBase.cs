using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryTest.Specification
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public virtual ISpecification<T> And(ISpecification<T> rightHand)
        {
            return new AndSpecification<T>(this, rightHand);
        }

        public virtual ISpecification<T> Or(ISpecification<T> rightHand)
        {
            return new OrSpecification<T>(this, rightHand);
        }

        public virtual ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}
