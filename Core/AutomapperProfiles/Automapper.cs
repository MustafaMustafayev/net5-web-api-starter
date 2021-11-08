using AutoMapper;
using DAL.Utility;
using DTO.DTOs;
using Entity.Entities;

namespace Core.AutomapperProfiles
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<UserToAddDTO, User>();
            CreateMap<UserToUpdateDTO, User>();
            CreateMap<User, UserToListDTO>();

            CreateMap<RoleToAddDTO, Role>();
            CreateMap<RoleToUpdateDTO, Role>();
            CreateMap<Role, RoleToListDTO>();
        }
    }
}
