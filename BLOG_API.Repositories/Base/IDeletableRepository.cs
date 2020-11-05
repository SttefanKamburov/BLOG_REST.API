using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLOG_API.Repositories.Base
{
    public class IDeletableRepository
    {
        public interface IDeleteableRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
        {
            IQueryable<TEntity> AllWithDeleted();

            TEntity GetWithDeleted(long id);
        }
    }
}
