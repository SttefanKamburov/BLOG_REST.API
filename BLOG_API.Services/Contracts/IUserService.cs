using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.Services.Base;
using BLOG_API.Shared.InputModels;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Services.Contracts
{
    public interface IUserService : IUserBaseService<UserDTO>
    {
        public Task<ICollection<UserDTO>> GetUserByCriteriaAsync(UsersCriteriaInputModel model);
    }
}
