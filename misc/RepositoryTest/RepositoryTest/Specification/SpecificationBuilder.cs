using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RepositoryTest.Specification
{
    public class SpecificationBuilder<T>
    {
        public static SpecificationBuilder<T> New
        {
            get
            {
                return new SpecificationBuilder<T>();
            }
        }

        public ISpecification<T> Specification { get; private set; }

        public SpecificationBuilder<T> With(Expression<Func<T, bool>> expression)
        {
            if (Specification != null) { throw new InvalidOperationException("Can't use with more than once"); }

            return new SpecificationBuilder<T>() { Specification = new LambdaSpecification<T>(expression) };
        }

        public SpecificationBuilder<T> And(Expression<Func<T, bool>> expression)
        {
            if (Specification == null) { throw new InvalidOperationException("Can't use 'And' without using 'With' once"); }

            ILambdaSpecification<T> lambda = Specification as ILambdaSpecification<T>;
            if (lambda != null)
            {
                return new SpecificationBuilder<T>() { Specification = lambda.And(expression) };
            }
            else
            {
                return new SpecificationBuilder<T>() { Specification = Specification.And(new LambdaSpecification<T>(expression)) };
            }
        }

        public SpecificationBuilder<T> Or(Expression<Func<T, bool>> expression)
        {
            if (Specification == null) { throw new InvalidOperationException("Can't use 'Or' without using 'With' once"); }

            ILambdaSpecification<T> lambda = Specification as ILambdaSpecification<T>;
            if (lambda != null)
            {
                return new SpecificationBuilder<T>() { Specification = lambda.Or(expression) };
            }
            else
            {
                return new SpecificationBuilder<T>() { Specification = Specification.Or(new LambdaSpecification<T>(expression)) };
            }
        }
    }
}
