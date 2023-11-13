using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Model;

namespace LabWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Software, SoftwareDto>();
            CreateMap<SoftwareDto, Software>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
        }
    }
}