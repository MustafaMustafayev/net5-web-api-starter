using DTO.DTOs;
using DTO.DTOs.Responses;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<string> GetUserSalt(string userName);
        Task<IDataResult<UserToListDTO>> Login(LoginDTO loginDTO);
    }
}
