using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLOG_API.DB.Models;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Mappers
{
    public class BlogMapper
    {
        public static readonly Expression<Func<Blog, BlogDTO>> SelectBlogDtoFromBlog = (blogModel) => new BlogDTO()
        {
            Id = blogModel.Id,
            IsDeleted = blogModel.IsDeleted,
            Name = blogModel.Name,
            UserCreatorId = blogModel.UserCreatorId,
            LastUserModifierId = blogModel.LastUserModifierId,
            DateCreated = blogModel.DataCreated,
            LastDateModified = blogModel.LastDateModified
        };
        public static void MapBlogFromBlogDto(ref Blog blog, ref BlogDTO blogDTO)
        {
            blog.Id = blogDTO.Id;
            blog.IsDeleted = blogDTO.IsDeleted;
            blog.Name = blogDTO.Name;
            blog.UserCreatorId = blogDTO.UserCreatorId;
            blog.LastUserModifierId = blogDTO.LastUserModifierId;
            blog.DataCreated = blogDTO.DateCreated;
            blog.LastDateModified = blogDTO.LastDateModified;
        }
    }
}
