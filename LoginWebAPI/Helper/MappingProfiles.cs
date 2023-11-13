using AutoMapper;
using LoginWebAPI.Dto;
using LoginWebAPI.Models;

namespace LoginWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserAdminDto>();
            CreateMap<UserAdminDto, User>();
            CreateMap<UserAdminPostDto, User>();
            CreateMap<User, UserClientDto>();
            CreateMap<UserClientDto, User>();
        }
    }
}