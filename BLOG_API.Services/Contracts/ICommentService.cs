﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Services.Contracts
{
    public interface ICommentService : IBaseService<CommentDTO>
    {
        public Task<ICollection<CommentDTO>> GetCommentsByCriteriaAsync(CommentCriteriaInputModel model);
        
    }
}
