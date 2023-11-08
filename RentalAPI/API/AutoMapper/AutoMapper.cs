using API.Models;
using AutoMapper;
using System.Globalization;

namespace API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<User, UserDto>();

        }
    }
}
