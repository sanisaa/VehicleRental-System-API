using API.Models;
using AutoMapper;

namespace API.AutoMapper
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

        }
    }
}
