using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.IServices;
using Core.Constants;
using Core.Utility;
using DAL.UnitOfWorks.IUnitOfWorks;
using DAL.Utility;
using DTO.DTOs;
using DTO.DTOs.Responses;
using Entity.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserToListDTO>> Add(UserToAddDTO userToAddDTO)
        {
            if(await _unitOfWork.UserRepository.IsUserExist(userToAddDTO.Username, null))
            {
                return new ErrorDataResult<UserToListDTO>(Messages.UsernameIsExist);
            }
            User user = _mapper.Map<User>(userToAddDTO);
            user.Salt = SecurityHelper.GenerateSalt();
            user.Password = SecurityHelper.HashPassword(user.Password, user.Salt);
            User added = await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.Commit();
            return new SuccessDataResult<UserToListDTO>(_mapper.Map<UserToListDTO>(added), Messages.Success);
        }

        public async Task Delete(int userId)
        {
            User user =  await _unitOfWork.UserRepository.Get(m => m.UserId == userId);
            user.IsDeleted = true;
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.Commit();
        }

        public async Task<IDataResult<PaginatedList<UserToListDTO>>> Get(int pageIndex, int pageSize)
        {
            IQueryable<User> users = await _unitOfWork.UserRepository.GetList();
            PaginatedList<User> response = await PaginatedList<User>.CreateAsync(users.OrderBy(m => m.UserId), pageIndex, pageSize);
            PaginatedList<UserToListDTO> responseDTO = new PaginatedList<UserToListDTO>(_mapper.Map<List<UserToListDTO>>(response.Datas), response.TotalRecordCount, response.PageIndex, response.TotalPageCount);
            return new SuccessDataResult<PaginatedList<UserToListDTO>>(responseDTO);
        }

        public async Task<IDataResult<UserToListDTO>> Get(int userId)
        {
            UserToListDTO user = _mapper.Map<UserToListDTO>(await _unitOfWork.UserRepository.Get(m => m.UserId == userId));
            return new SuccessDataResult<UserToListDTO>(user);
        }

        public async Task<IDataResult<UserToListDTO>> Update(UserToUpdateDTO userToUpdateDTO)
        {
            if (await _unitOfWork.UserRepository.IsUserExist(userToUpdateDTO.Username, userToUpdateDTO.UserId))
            {
                return new ErrorDataResult<UserToListDTO>(Messages.UsernameIsExist);
            }
            User user = _mapper.Map<User>(userToUpdateDTO);
            user.Salt = SecurityHelper.GenerateSalt();
            user.Password = SecurityHelper.HashPassword(user.Password, user.Salt);
            User updated = await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.Commit();
            return new SuccessDataResult<UserToListDTO>(_mapper.Map<UserToListDTO>(updated), Messages.Success);
        }
    }
}
