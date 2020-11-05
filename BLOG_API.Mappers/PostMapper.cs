using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLOG_API.DB.Models;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Mappers
{
    public class PostMapper
    {
        public static readonly Expression<Func<Post, PostDTO>> SelectPostDtoFromPost= (postModel) => new PostDTO()
        {
            Id = postModel.Id,
            IsDeleted = postModel.IsDeleted,
            Title = postModel.Title,
            Text = postModel.Text,
            Rate = postModel.Rate,
            BlogId = postModel.BlogId,
            UserCreatorId = postModel.UserCreatorId,
            LastUserModifierId = postModel.LastUserModifierId,
            DateCreated = postModel.DataCreated,
            LastDateModified = postModel.LastDateModified
        };
        public static void MapPostFromPostDto(ref Post post, ref PostDTO postDTO)
        {
            post.Id = postDTO.Id;
            post.IsDeleted = postDTO.IsDeleted;
            post.Title = postDTO.Title;
            post.Text = postDTO.Text;
            post.Rate = postDTO.Rate;
            post.BlogId = postDTO.BlogId;
            post.UserCreatorId = postDTO.UserCreatorId;
            post.LastUserModifierId = postDTO.LastUserModifierId;
            post.DataCreated = postDTO.DateCreated;
            post.LastDateModified = postDTO.LastDateModified;
        }
    }
}
