using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryTest.Specification;

namespace RepositoryTest.Repository
{
    public interface ILambdaRepository<T> : IRepository<T>, IQueryable<T> where T : IKey
    {
        void DeleteAll(ILambdaSpecification<T> specification);

        IQueryable<T> Find(ILambdaSpecification<T> specification);
    }
}
