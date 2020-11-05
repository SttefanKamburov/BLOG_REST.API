using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using static BLOG_API.Repositories.Base.IDeletableRepository;

namespace BLOG_API.Repositories.Base
{
    public class DeletableUserRepository<TEntity> : BaseUserRepository<TEntity>, IDeleteableRepository<TEntity>
        where TEntity : User
    {
        public DeletableUserRepository(BlogDbContext context) : base(context)
        {
        }

        public override IQueryable<TEntity> All()
        {
            return this.DbSet.Where(e => !e.IsDeleted);
        }

        public override IQueryable<TEntity> Get(long id)
        {
            return this.DbSet.Where(e => !e.IsDeleted && e.Id == id);
        }

        public IQueryable<TEntity> AllWithDeleted()
        {
            return this.DbSet;
        }

        public TEntity GetWithDeleted(long id)
        {
            return this.DbSet.FirstOrDefault(e => e.Id == id);
        }

        public override void Delete(long id)
        {
            var entity = this.Get(id).FirstOrDefault();

            if (entity == null)
            {
                throw new ArgumentNullException("entity for delete was not found!");
            }

            entity.IsDeleted = true;
            this.Update(entity);
        }
    }
}

