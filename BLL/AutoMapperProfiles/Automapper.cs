using System;
using AutoMapper;
using DTO.DTOs;
using Entity.Entities;

namespace BLL.AutoMapperProfiles
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
