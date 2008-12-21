using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryTest.Specification
{
    public class OrSpecification<T> : SpecificationBase<T>
    {
        private ISpecification<T> m_left;
        private ISpecification<T> m_right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            if (left == null) { throw new ArgumentNullException("left", "left is null."); }
            if (right == null) { throw new ArgumentNullException("right", "right is null."); }

            m_left = left;
            m_right = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return m_left.IsSatisfiedBy(entity) || m_right.IsSatisfiedBy(entity);
        }
    }
}
