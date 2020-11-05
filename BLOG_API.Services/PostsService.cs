using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using BLOG_API.Mappers;
using BLOG_API.Repositories;
using BLOG_API.Services.Base;
using BLOG_API.Services.Contracts;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace BLOG_API.Services
{
    public class PostsService : BaseService, IPostsService
    {
        private readonly DeletableRepository<Post> repository;

        public PostsService(BlogDbContext context)
        {
            this.repository = new DeletableRepository<Post>(context);
        }
        public async Task<PostDTO> AddAsync(PostDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            Post post = new Post();
            dtoModel.DateCreated = DateTime.Now;
            dtoModel.LastDateModified = DateTime.Now;
            PostMapper.MapPostFromPostDto(ref post, ref dtoModel);
            this.repository.Add(post);
            await this.repository.SaveChangesAsync();
            dtoModel.Id = post.Id;
            return dtoModel;
        }

        public async Task<ICollection<PostDTO>> AllAsync()
        {
            if (this.repository.All() == null)
            {
                throw new Exception("No blogs in database");
            }
            return await this.repository.All().Select(PostMapper.SelectPostDtoFromPost).ToListAsync();
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

        public async Task<ICollection<PostDTO>> GetPostsByCriteria(PostsCriteriaInputModel model)
        {
            if (!model.Ids.Any() &&
                string.IsNullOrWhiteSpace(model.Title) &&
                string.IsNullOrWhiteSpace(model.Text) &&
                model.RateEquals == null &&
                model.RateGreater == null &&
                model.RateLower == null &&
                !model.UserIds.Any() &&
                model.DateCreated == null &&
                model.BlogId == null
                )
            {
                throw new ArgumentNullException("No criteria typed");
            }
            var posts = this.repository.All();

            if (model.Ids.Any())
            {
                posts = posts
                    .Where(p => model.Ids.Any(m => m == p.Id));
            }

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                posts = posts
                    .Where(p => p.Title.Contains(model.Title));
            }

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                posts = posts
                    .Where(p => p.Text.Contains(model.Text));
            }

            if (model.RateEquals != null)
            {
                posts = posts
                    .Where(p => p.Rate == model.RateEquals);
            }

            if (model.RateLower != null)
            {
                posts = posts
                    .Where(p => p.Rate < model.RateLower);
            }

            if (model.RateGreater != null) 
            {
                posts = posts
                    .Where(p => p.Rate > model.RateGreater);
            }

            if (model.UserIds.Any()) 
            {
                posts = posts
                  .Where(p => model.UserIds.Contains((long)p.UserCreatorId));
            }
    
            if (model.DateCreated != null) 
            {
                posts = posts
                    .Where(p => p.DataCreated.Date == model.DateCreated.Value.Date);
            }

            if (model.BlogId != null) 
            {
                posts = posts
                    .Where(p => p.BlogId == model.BlogId);
            }

            return await posts.Select(PostMapper.SelectPostDtoFromPost).ToListAsync();
        }

        public async Task<PostDTO> GetAsync(long id)
        {
            return await this.repository.Get(id).Select(PostMapper.SelectPostDtoFromPost).FirstOrDefaultAsync();
        }

        public async Task<PostDTO> UpdateAsync(PostDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            var postEntity = await this.repository.Get(dtoModel.Id).FirstOrDefaultAsync();
            if (postEntity == null)
            {
                throw new Exception("Blog for update not found");
            }
            dtoModel.LastDateModified = DateTime.Now;
            PostMapper.MapPostFromPostDto(ref postEntity, ref dtoModel);
            this.repository.Update(postEntity);
            await this.repository.SaveChangesAsync();
            return dtoModel;
        }

        public async Task ValidateAsync(PostDTO post)
        {

            if (post == null || post.Id < 0)
            {
                throw new Exception("Post not found");
            }

            if (await this.repository.All().AnyAsync(p => p.Title.Equals(post.Title)))
            {
                throw new Exception("Post already exists");
            }
        }
    }
}
