using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.IServices;
using Core.Constants;
using DAL.UnitOfWorks.IUnitOfWorks;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Entity.Entities;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> GetUserSalt(string userName)
        {
            return await _unitOfWork.UserRepository.GetUserSalt(userName);
        }

        public async Task<IDataResult<UserToListDTO>> Login(LoginDTO loginDTO)
        {
            User user = await _unitOfWork.UserRepository.GetAsync(m => m.Username == loginDTO.Username && m.Password == loginDTO.Password);
            if(user == null)
            {
                return new ErrorDataResult<UserToListDTO>(Messages.InvalidUserCredentials);
            }
            return new SuccessDataResult<UserToListDTO>(_mapper.Map<UserToListDTO>(user));
        }
    }
}
