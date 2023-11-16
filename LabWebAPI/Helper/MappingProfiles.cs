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
            CreateMap<LabUser, LabUserDto>();
            CreateMap<LabUserDto, LabUser>();
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
        }
    }
}