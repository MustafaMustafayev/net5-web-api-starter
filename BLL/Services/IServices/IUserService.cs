using DAL.Utility;
using DTO.DTOs;
using DTO.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService
    {
        Task<IDataResult<PaginatedList<UserToListDTO>>> Get(int pageIndex, int pageSize);
        Task<IDataResult<UserToListDTO>> Get(int userId);
        Task<IDataResult<UserToListDTO>> Add(UserToAddDTO userToAddDTO);
        Task<IDataResult<UserToListDTO>> Update(UserToUpdateDTO userToUpdateDTO);
        Task Delete(int userId);
    }
}
