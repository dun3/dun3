using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using RepositoryTest.Specification;

namespace RepositoryTest.Repository
{
    public abstract class LambdaRepositoryBase<T> : ILambdaRepository<T> where T : IKey
    {
        protected abstract IQueryable<T> RepositoryQuery { get; }

        #region IRepository<T> Members

        public abstract void Save(T entity);

        public abstract void Delete(T entity);

        public virtual void DeleteAll(ISpecification<T> specification)
        {
            foreach (T entity in Find(specification))
            {
                Delete(entity);
            }
        }

        public virtual void DeleteAll(ILambdaSpecification<T> specification)
        {
            foreach (T entity in Find(specification))
            {
                Delete(entity);
            }
        }

        public IEnumerable<T> Find(ISpecification<T> specification)
        {
            return from s in RepositoryQuery
                   where specification.IsSatisfiedBy(s)
                   select s;
        }

        public IQueryable<T> Find(ILambdaSpecification<T> specification)
        {
            return RepositoryQuery.Where(specification.Predicate);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return RepositoryQuery.ElementType; }
        }

        public Expression Expression
        {
            get { return RepositoryQuery.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return RepositoryQuery.Provider; }
        }

        #endregion
    }
}
