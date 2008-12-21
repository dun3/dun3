using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryTest.Repository;
using RepositoryTest.Specification;

namespace RepositoryTest
{
    public abstract class MapperRepository<T> : LambdaRepositoryBase<T> where T : IKey
    {
        protected readonly ILambdaMapper<T> m_mapper;

        protected MapperRepository(ILambdaMapper<T> mapper)
        {
            if (mapper == null) { throw new ArgumentNullException("mapper"); }

            m_mapper = mapper;
        }

        protected override IQueryable<T> RepositoryQuery
        {
            get { return m_mapper.FindAll(LambdaSpecification<T>.ALL); }
        }

        public override void Save(T entity)
        {
            m_mapper.Save(entity);
        }

        public override void Delete(T entity)
        {
            Delete(entity);
        }
    }
}
