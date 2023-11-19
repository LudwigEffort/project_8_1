using AutoMapper;
using LabWebAPI.Controllers;
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
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.Softwares, opt => opt.MapFrom(src => src.ItemSoftwares.Select(isr => isr.Software)));
            CreateMap<ItemDto, Item>();
            CreateMap<ItemPostDto, Item>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
        }
    }
}