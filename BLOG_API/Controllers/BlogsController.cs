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
    public class BlogsController : BaseController
    {
        IBlogService service;

        public BlogsController(IBlogService service)
        {
            this.service=service;
        }

        [HttpGet]
        public async Task<ICollection<BlogDTO>> GetBlogs()
        {
            return await service.AllAsync();
        }

        [HttpGet("/api/GetBlogById/{id}")]
        public async Task<BlogDTO> GetBlogById(long id)
        {
            return await service.GetAsync(id);
        }

        [HttpGet("/api/GetBlogByCriteria")]
        public async Task<ICollection<BlogDTO>> GetBlogsByCriteria([FromBody] BlogsCriteriaInputModel blog)
        {
            return await service.GetBlogByCriteriaAsync(blog);
        }

        [HttpPut]
        public async Task<BlogDTO> UpdateBlogAsync([FromBody] BlogDTO blog)
        {
            return await service.UpdateAsync(blog);
        }

        [HttpPost]
        public async Task<BlogDTO> AddBlogAsync([FromBody] BlogDTO blog)
        {
            return await service.AddAsync(blog);
        }

        [HttpDelete("{id}")]
        public async Task<String> DeleteBlog(long id)
        {
            if (await service.DeleteAsync(id))
            {
                return "success";
            }
            return "failed";
        }
    }
}
