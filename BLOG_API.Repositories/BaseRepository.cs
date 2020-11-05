using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using Microsoft.EntityFrameworkCore;


namespace BLOG_API.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly BlogDbContext context;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(BlogDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }
        protected BlogDbContext Context
        {
            get
            {
                return this.context;
            }
        }
        protected DbSet<TEntity> DbSet
        {
            get
            {
                return this.dbSet;
            }
        }
        public virtual void Add(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }
        public virtual void Delete(long id)
        {
            var entity = this.Get(id).FirstOrDefault();
            var entry = this.Context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Remove(entity);
            }
        }
        public virtual IQueryable<TEntity> Get(long id)
        {
            return this.DbSet.Where(e => e.Id == id);
        }
        public virtual IQueryable<TEntity> All()
        {
            return this.DbSet;
        }
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity for update was not found!");
            }

            this.Context.Entry(entity).State = EntityState.Modified;
        }
        public virtual async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await this.Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool SaveChanges()
        {
            return this.Context.SaveChanges() > 0;
        }
        public void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
    }
}
