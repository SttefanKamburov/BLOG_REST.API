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
    public class UsersController : BaseController
    {
        IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ICollection<UserDTO>> GetUsers()
        {
            return await service.AllAsync();
        }

        [HttpGet("/api/GetUserById/{id}")]
        public async Task<UserDTO> GetUserById(long id)
        {
            return await service.GetAsync(id);
        }

        [HttpGet("/api/GetUserByCriteria")]
        public async Task<ICollection<UserDTO>> GetUserByName([FromBody] UsersCriteriaInputModel user)
        {
            return await service.GetUserByCriteriaAsync(user);
        }

        [HttpPost]
        public async Task<UserDTO> AddUserAsync([FromBody] UserDTO user)
        {
            return await service.AddAsync(user);
        }

        [HttpPut]
        public async Task<UserDTO> UpdateUserAsync([FromBody] UserDTO user) 
        {
            return await service.UpdateAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task<String> DeleteUser(long id)
        {
            if (await service.DeleteAsync(id))
            {
                return "success";
            }
            return "failed";
        }

    }


}
