using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLOG_API.DB.Models;
using BLOG_API.Shared.ModelsDTO;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace BLOG_API.Mappers
{
    public class UserMapper
    {
        public static readonly Expression<Func<User, UserDTO>> SelectUserDtoFromUser = (userModel) => new UserDTO()
        {
            Id = userModel.Id,
            IsDeleted = userModel.IsDeleted,
            Name = userModel.Name,
            Username = userModel.Username,
            Password = userModel.Password,
            Email = userModel.Email,
            UserCreatorId = userModel.UserCreatorId,
            UserLastModifiedId = userModel.UserLastModifiedId,
            DateCreated = userModel.DateCreated,
            DateLastModified = userModel.DateLastModified,
        };
        public static void MapUserFromUserDto(ref User user, ref UserDTO userDTO)
        {
            user.Id = userDTO.Id;
            user.IsDeleted = userDTO.IsDeleted;
            user.Name = userDTO.Name;
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.UserCreatorId = (long)userDTO.UserCreatorId;
            user.UserLastModifiedId = (long)userDTO.UserLastModifiedId;
            user.DateLastModified = userDTO.DateLastModified;
            user.DateCreated = userDTO.DateCreated;
        }

    }
}
