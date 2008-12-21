using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryTest.Specification;

namespace RepositoryTest.Repository
{
    public interface IRepository<T> where T : IKey
    {
        void Save(T entity);

        void Delete(T entity);
        void DeleteAll(ISpecification<T> specification);

        IEnumerable<T> Find(ISpecification<T> specification);
    }
}
