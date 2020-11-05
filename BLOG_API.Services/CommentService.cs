using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using BLOG_API.Mappers;
using BLOG_API.Repositories;
using BLOG_API.Services.Base;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace BLOG_API.Services.Contracts
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly DeletableRepository<Comment> repository;
        public CommentService(BlogDbContext context)
        {
            this.repository = new DeletableRepository<Comment>(context);
        }
        public async Task<CommentDTO> AddAsync(CommentDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            Comment comment = new Comment();
            dtoModel.DateCreated = DateTime.Now;
            dtoModel.LastDateModified = DateTime.Now;
            CommentMapper.MapCommentFromCommentDto(ref comment, ref dtoModel);
            this.repository.Add(comment);
            await this.repository.SaveChangesAsync();
            dtoModel.Id = comment.Id;
            return dtoModel;
        }
        public async Task<ICollection<CommentDTO>> AllAsync()
        {
            if (this.repository.All() == null)
            {
                throw new Exception("No blogs in database");
            }
            return await this.repository.All().Select(CommentMapper.SelectCommentDtoFromComment).ToListAsync();
        }
        public async Task<bool> DeleteAsync(long id)
        {
            if (this.repository.Get(id).FirstOrDefault() == null)
            {
                throw new Exception("Comment for deletion not found");
            }
            this.repository.Delete(id);
            return await this.repository.SaveChangesAsync();
        }
        public async Task<CommentDTO> GetAsync(long id)
        {
            return await this.repository.Get(id).Select(CommentMapper.SelectCommentDtoFromComment).FirstOrDefaultAsync();
        }
        public async Task<ICollection<CommentDTO>> GetCommentsByCriteriaAsync(CommentCriteriaInputModel model)
        {
            if (!model.CommentIds.Any() &&
                !model.PostIds.Any() &&
                string.IsNullOrWhiteSpace(model.Text) &&
                model.RateEquals == null &&
                model.RateLower == null &&
                model.RateGreater == null &&
                !model.UserIds.Any() &&
                model.DateCreated == null &&
                model.BlogId == null
                )
            {
                throw new ArgumentNullException("Critera is empty");
            }

            var comments = this.repository.All();

            if (model.CommentIds.Any())
            {
                comments = comments
                    .Where(c => model.CommentIds.Contains(c.Id));
            }

            if (model.PostIds.Any())
            {
                comments = comments
                    .Where(c => model.PostIds.Contains(c.PostId));
            }

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                comments = comments
                    .Where(c => c.Text.Contains(model.Text));
            }

            if (model.RateEquals != null)
            {
                comments = comments
                    .Where(c => c.Rate == model.RateEquals);
            }

            if (model.RateGreater != null)
            {
                comments = comments
                    .Where(c => c.Rate > model.RateGreater);
            }

            if (model.RateLower != null)
            {
                comments = comments
                    .Where(c => c.Rate < model.RateLower);
            }

            if (model.UserIds.Any())
            {
                comments = comments
                    .Where(c => model.UserIds.Contains((long)c.UserCreatorId));
            }

            if (model.DateCreated != null)
            {
                comments = comments
                     .Where(c => c.DataCreated.Date == model.DateCreated.Value.Date);
            }

            if (model.BlogId != null)
            {
                comments = comments
                    .Where(c => c.Post.BlogId == model.BlogId);
            }
            return await comments.Select(CommentMapper.SelectCommentDtoFromComment).ToListAsync();
        }
        public async Task<CommentDTO> UpdateAsync(CommentDTO dtoModel)
        {
            await this.ValidateAsync(dtoModel);
            var commentEntity = await this.repository.Get(dtoModel.Id).FirstOrDefaultAsync();
            if (commentEntity == null)
            {
                throw new Exception("Comment for update not found");
            }
            dtoModel.LastDateModified = DateTime.Now;
            CommentMapper.MapCommentFromCommentDto(ref commentEntity, ref dtoModel);
            this.repository.Update(commentEntity);
            await this.repository.SaveChangesAsync();
            return dtoModel;
        }
        public async Task ValidateAsync(CommentDTO comment)
        {
            if (comment == null || comment.Id < 0)
            {
                throw new Exception("Comment not found");
            }
        }
    }
}
