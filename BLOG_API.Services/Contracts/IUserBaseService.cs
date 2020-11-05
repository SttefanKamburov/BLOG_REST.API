using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLOG_API.Shared.ModelsDTO;

namespace BLOG_API.Services.Contracts
{
    public interface IUserBaseService<TDto>
        where TDto : UserDTO
    {
        Task<ICollection<TDto>> AllAsync();
        Task<TDto> GetAsync(long id);
        Task<TDto> AddAsync(TDto dtoModel);
        Task<TDto> UpdateAsync(TDto dtoModel);
        Task<bool> DeleteAsync(long id);
        Task ValidateAsync(TDto dtoModel);
    }
}
