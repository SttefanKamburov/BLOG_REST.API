using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.DB.Models;
using BLOG_API.Mappers;
using BLOG_API.Repositories;
using BLOG_API.Repositories.Base;
using BLOG_API.Services.Base;
using BLOG_API.Services.Contracts;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BLOG_API.Services
{
    public class UsersService : BaseService, IUserService
    {
        private readonly DeletableUserRepository<User> repository;
        public UsersService(BlogDbContext context)
        {
            this.repository = new DeletableUserRepository<User>(context);
        }
        public async Task<UserDTO> AddAsync(UserDTO user)
        {
            await this.ValidateAsync(user);
            user.DateCreated = DateTime.Now;
            user.DateLastModified = DateTime.Now;
            user.Password = CreateMD5(user.Password);
            User newUser = new User();
            UserMapper.MapUserFromUserDto(ref newUser, ref user);
            this.repository.Add(newUser);
            await this.repository.SaveChangesAsync();
            user.Id = newUser.Id;
            return user;
        }
        public async Task<ICollection<UserDTO>> AllAsync()
        {
            if (this.repository.All() == null)
            {
                throw new Exception("No users in database");
            }
            return await this.repository.All().Select(UserMapper.SelectUserDtoFromUser).ToListAsync();
        }
        public async Task<bool> DeleteAsync(long id)
        {
            if (this.repository.All().Where(u => u.Id == id).FirstOrDefault() == null)
            {
                throw new Exception("User for deletion not found");
            }
            this.repository.Delete(id);
            return await repository.SaveChangesAsync();
        }
        public async Task<UserDTO> GetAsync(long id)
        {
            UserDTO user = await this.repository.All()
                .Select(UserMapper.SelectUserDtoFromUser)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("No such user found");
            }
            return user;
        }
        public async Task<ICollection<UserDTO>> GetUserByCriteriaAsync(UsersCriteriaInputModel model)
        {
            if (!model.UserIds.Any() &&
                model.DateCreated == null &&
                model.DateLastModified == null &&
                model.UserCreatorId == null &&
                string.IsNullOrWhiteSpace(model.Username) &&
                string.IsNullOrWhiteSpace(model.Email) &&
                string.IsNullOrWhiteSpace(model.Name) &&
                !model.BlogIds.Any() &&
                !model.CommentIds.Any() &&
                !model.PostIds.Any()
                )
            {
                throw new ArgumentNullException("No Input criteria typed");
            }
            var users = this.repository.All();

            if (model.UserIds.Any())
            {
                users = users
                    .Where(u =>model.UserIds.Contains(u.Id));
            }

            if (model.DateCreated != null)
            {
                users = users
                     .Where(u => u.DateCreated.Date == model.DateCreated.Value.Date);
            }

            if (model.DateLastModified != null)
            {
                users = users
                     .Where(u => u.DateLastModified.Date == model.DateLastModified.Value.Date);
            }

            if (model.UserCreatorId != null)
            {
                users = users
                    .Where(u => u.UserCreatorId == model.UserCreatorId);
            }

            if (!string.IsNullOrWhiteSpace(model.Username))
            {
                users = users
                    .Where(u => u.Username.Contains(model.Username));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                users = users
                    .Where(u => u.Email.Contains(model.Email));
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                users = users
                    .Where(u => u.Name.Contains(model.Name));
            }

            if (model.BlogIds.Any())
            {
                users = users
                    .Where(u => u.BlogsCreated.Any(b =>model.BlogIds.Any(m => m == b.Id)));
            }

            if (model.CommentIds.Any())
            {
                users = users
                        .Where(u => u.Comments.Any(c =>model.CommentIds.Any(m => m == c.Id)));
            }

            if (model.PostIds.Any())
            {
                users = users
                    .Where(u => u.Posts.Any(p => model.PostIds.Any(m => m == p.Id)));
            }
            return await users.Select(UserMapper.SelectUserDtoFromUser).ToListAsync();
        }
        public async Task<UserDTO> UpdateAsync(UserDTO user)
        {
            user.Password = CreateMD5(user.Password);
            user.DateLastModified = DateTime.Now;
            var userEntity = await this.repository.Get(user.Id).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                throw new Exception("No such user found");
            }
            UserMapper.MapUserFromUserDto(ref userEntity, ref user);
            this.repository.Update(userEntity);
            await this.repository.SaveChangesAsync();
            return user;
        }
        public async Task ValidateAsync(UserDTO user)
        {
            if (user == null || user.Id < 0)
            {
                throw new Exception("User not found");
            }

            if (await this.repository.All().AnyAsync(c => c.Id != user.Id && c.Username.Equals(user.Username) || c.Email.Equals(user.Email)))
            {
                throw new Exception("User already exists");
            }
        }
        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}


