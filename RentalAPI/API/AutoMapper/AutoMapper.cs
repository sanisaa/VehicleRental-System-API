using AutoMapper;
using Domain.DTO;
using Domain.Model;

namespace API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()));

            CreateMap<LoginDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
