using BLL.Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.UnitOfWorks.IUnitOfWorks;
using DTO.DTOs.Responses;
using DTO.DTOs;
using Entity.Entities;
using Core.Constants;
using System.Linq;
using DAL.Utility;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<RoleToListDTO>> Add(RoleToAddDTO roleToAddDTO)
        {
            Role role = _mapper.Map<Role>(roleToAddDTO);
            Role added = await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<RoleToListDTO>(_mapper.Map<RoleToListDTO>(added), Messages.Success);
        }

        public async Task Delete(int roleId)
        {
            Role role = await _unitOfWork.RoleRepository.GetAsync(m => m.RoleId == roleId);
            role.IsDeleted = true;
            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IDataResult<PaginatedList<RoleToListDTO>>> Get(int pageIndex, int pageSize)
        {
            IQueryable<Role> roles =  _unitOfWork.RoleRepository.GetAsNoTrackingList();
            PaginatedList<Role> response = await PaginatedList<Role>.CreateAsync(roles.OrderBy(m => m.RoleId), pageIndex, pageSize);
            PaginatedList<RoleToListDTO> responseDTO = new PaginatedList<RoleToListDTO>(_mapper.Map<List<RoleToListDTO>>(response.Datas), response.TotalRecordCount, response.PageIndex, response.TotalPageCount);
            return new SuccessDataResult<PaginatedList<RoleToListDTO>>(responseDTO);
        }

        public async Task<IDataResult<RoleToListDTO>> Get(int roleId)
        {
            RoleToListDTO role = _mapper.Map<RoleToListDTO>(await _unitOfWork.RoleRepository.GetAsNoTrackingAsync(m => m.RoleId == roleId));
            return new SuccessDataResult<RoleToListDTO>(role);
        }

        public async Task<IDataResult<RoleToListDTO>> Update(RoleToUpdateDTO roleToUpdateDTO)
        {
            Role role = _mapper.Map<Role>(roleToUpdateDTO);
            Role updated = _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<RoleToListDTO>(_mapper.Map<RoleToListDTO>(updated), Messages.Success);
        }
    }
}
