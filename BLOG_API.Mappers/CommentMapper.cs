using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLOG_API.DB.Models;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Mappers
{
    public class CommentMapper
    {
        public static readonly Expression<Func<Comment, CommentDTO>> SelectCommentDtoFromComment = (commentModel) => new CommentDTO()
        {
           Id = commentModel.Id,
           IsDeleted = commentModel.IsDeleted,
           Text = commentModel.Text,
           PostId = commentModel.PostId,
           Rate = commentModel.Rate,
           UserCreatorId = commentModel.UserCreatorId,
           LastUserModifierId = commentModel.LastUserModifierId,
           DateCreated = commentModel.DataCreated,
           LastDateModified = commentModel.LastDateModified
        };
        public static void MapCommentFromCommentDto(ref Comment comment, ref CommentDTO commentDTO)
        {
            comment.Id = commentDTO.Id;
            comment.IsDeleted = commentDTO.IsDeleted;
            comment.Text = commentDTO.Text;
            comment.PostId = commentDTO.PostId;
            comment.Rate = commentDTO.Rate;
            comment.UserCreatorId= (long)commentDTO.UserCreatorId;
            comment.LastUserModifierId = commentDTO.LastUserModifierId;
            comment.DataCreated = commentDTO.DateCreated;
            comment.LastDateModified = comment.LastDateModified;
        }
    }
}
