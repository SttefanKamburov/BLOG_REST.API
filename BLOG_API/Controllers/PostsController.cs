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
    public class PostsController : BaseController
    {
        private readonly IPostsService service;

        public PostsController(IPostsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ICollection<PostDTO>> GetPosts()
        {
            return await service.AllAsync();
        }

        [HttpGet("/api/GetPostById/{id}")]
        public async Task<PostDTO> GetPostById(long id)
        {
            return await service.GetAsync(id);
        }

        [HttpGet("/api/GetPostByCriteria")]
        public async Task<ICollection<PostDTO>> GetBlogsByCriteria([FromBody] PostsCriteriaInputModel post)
        {
            return await service.GetPostsByCriteria(post);
        }

        [HttpPut]
        public async Task<PostDTO> UpdatePostAsync([FromBody] PostDTO post)
        {
            return await service.UpdateAsync(post);
        }

        [HttpPost]
        public async Task<PostDTO> AddPostAsync([FromBody] PostDTO post)
        {
            return await service.AddAsync(post);
        }

        [HttpDelete("{id}")]
        public async Task<String> DeletePost(long id)
        {
            if (await service.DeleteAsync(id))
            {
                return "success";
            }
            return "failed";
        }
    }
}