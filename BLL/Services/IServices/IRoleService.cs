using DAL.Utility;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Entity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IRoleService
    {
        Task<IDataResult<PaginatedList<RoleToListDTO>>> Get(int pageIndex, int pageSize);
        Task<IDataResult<RoleToListDTO>> Get(int roleId);
        Task<IDataResult<RoleToListDTO>> Add(RoleToAddDTO roleToAddDTO);
        Task<IDataResult<RoleToListDTO>> Update(RoleToUpdateDTO roleToUpdateDTO);
        Task Delete(int roleId);
    }
}
