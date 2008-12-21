using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryTest.Specification
{
    public class NotSpecification<T> : SpecificationBase<T>
    {
        private ISpecification<T> m_inner;

        public NotSpecification(ISpecification<T> inner)
        {
            if (inner == null) { throw new ArgumentNullException("inner"); }

            m_inner = inner;
        }
        public override bool IsSatisfiedBy(T entity)
        {
            return !m_inner.IsSatisfiedBy(entity);
        }
    }
}
