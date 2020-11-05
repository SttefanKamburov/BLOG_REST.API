using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using BLOG_API.Services.Contracts;
using BLOG_API.Shared.ModelsDTO;
using BLOG_API.Shared.InputModels;

namespace BLOG_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService service;

        public CommentsController(ICommentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ICollection<CommentDTO>> GetPosts()
        {
            return await service.AllAsync();
        }

        [HttpGet("/api/GetCommentById/{id}")]
        public async Task<CommentDTO> GetCommentById(long id)
        {
            return await service.GetAsync(id);
        }

        [HttpGet("/api/GetCommentByCriteria")]
        public async Task<ICollection<CommentDTO>> GetCommentsByCriteria([FromBody] CommentCriteriaInputModel comment)
        {
            return await service.GetCommentsByCriteriaAsync(comment);
        }

        [HttpPut]
        public async Task<CommentDTO> UpdatePostAsync([FromBody] CommentDTO comment)
        {
            return await service.UpdateAsync(comment);
        }

        [HttpPost]
        public async Task<CommentDTO> AddPostAsync([FromBody] CommentDTO comment)
        {
            return await service.AddAsync(comment);
        }

        [HttpDelete("{id}")]
        public async Task<String> DeleteComment(long id)
        {
            if (await service.DeleteAsync(id))
            {
                return "success";
            }
            return "failed";
        }
    }
}
