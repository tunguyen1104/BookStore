using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
             CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.Avatar, opt => opt.Ignore())
            .ForMember(dest => dest.Phone, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Carts, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
