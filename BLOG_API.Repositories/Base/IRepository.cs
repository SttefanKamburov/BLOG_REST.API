using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG_API.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        IQueryable<T> Get(long id);

        void Add(T entity);

        void Update(T entity);

        void Delete(long id);

        Task<bool> SaveChangesAsync();
    }
}
