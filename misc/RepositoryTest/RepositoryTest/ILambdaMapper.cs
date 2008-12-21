using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryTest.Specification;

namespace RepositoryTest
{
    public interface ILambdaMapper<T> where T : IKey
    {
        T Get(IKey id);

        IQueryable<T> FindAll(ILambdaSpecification<T> specification);

        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Update(T entity);

        void Delete(T entity);
        void DeleteAll(ILambdaSpecification<T> specification);
    }
}
