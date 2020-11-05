using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using BLOG_API.Mappers;
using BLOG_API.Repositories;
using BLOG_API.Repositories.Base;
using BLOG_API.Services.Base;
using BLOG_API.Services.Contracts;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BLOG_API.Services
{
    public class BlogsService : BaseService, IBlogService
    {
        private readonly DeletableRepository<Blog> repository;

        public BlogsService(BlogDbContext context)
        {
            this.repository = new DeletableRepository<Blog>(context);
        }
        public async Task<BlogDTO> AddAsync(BlogDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            Blog blog = new Blog();
            dtoModel.DateCreated = DateTime.Now;
            dtoModel.LastDateModified = DateTime.Now;
            BlogMapper.MapBlogFromBlogDto(ref blog, ref dtoModel);
            this.repository.Add(blog);
            await this.repository.SaveChangesAsync();
            dtoModel.Id = blog.Id;
            return dtoModel;
        }
        public async Task<ICollection<BlogDTO>> AllAsync()
        {
            if (this.repository.All() == null) 
            {
                throw new Exception("No blogs in database");            
            }
            return await this.repository.All().Select(BlogMapper.SelectBlogDtoFromBlog).ToListAsync();
        }
        public async Task<bool> DeleteAsync(long id)
        {
            if (this.repository.Get(id).FirstOrDefault() == null) 
            {
                throw new Exception("Blog for deletion not found");
            }
            this.repository.Delete(id);
            return await this.repository.SaveChangesAsync();
        }
        public async Task<BlogDTO> GetAsync(long id)
        {
            return await this.repository.Get(id).Select(BlogMapper.SelectBlogDtoFromBlog).FirstOrDefaultAsync();
        }
        public async Task<ICollection<BlogDTO>> GetBlogByCriteriaAsync(BlogsCriteriaInputModel model)
        {
            if (model.PostIds.Any() &&
                model.UsersIds.Any() &&
                model.DateCreated == null &&
                string.IsNullOrWhiteSpace(model.Name) 
                ) 
            {
                throw new ArgumentNullException("Critera is empty");
            }
            var blogs = this.repository.All();

            if (model.UsersIds.Any()) 
            {
                blogs = blogs
                   .Where(u => model.UsersIds.Contains((long)u.UserCreatorId));
            }

            if (model.PostIds.Any()) 
            {
                blogs = blogs
                    .Where(b => b.Posts.Any(p => model.PostIds.Contains(p.Id)));
            }

            if (model.DateCreated != null) 
            {
                blogs = blogs
                     .Where(b => b.DataCreated.Date == model.DateCreated.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(model.Name)) 
            {
                blogs = blogs
                    .Where(b => b.Name.Contains(model.Name));
            }
            return await blogs.Select(BlogMapper.SelectBlogDtoFromBlog).ToListAsync();
        }
        public async Task<BlogDTO> UpdateAsync(BlogDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            var blogEntity = await this.repository.Get(dtoModel.Id).FirstOrDefaultAsync();
            if (blogEntity == null) 
            {
                throw new Exception("Blog for update not found");
            }
            dtoModel.LastDateModified = DateTime.Now;
            BlogMapper.MapBlogFromBlogDto(ref blogEntity, ref dtoModel);
            this.repository.Update(blogEntity);
            await this.repository.SaveChangesAsync();
            return dtoModel;
        }
        public async Task ValidateAsync(BlogDTO blog)
        {
            if (blog == null || blog.Id < 0)
            {
                throw new Exception("Blog not found");
            }

            if (await this.repository.All().AnyAsync(b => b.Name.Equals(blog.Name)))
            {
                throw new Exception("Blog already exists");
            }
        }
    }
}
