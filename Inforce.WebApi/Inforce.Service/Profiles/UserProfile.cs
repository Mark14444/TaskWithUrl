using AutoMapper;
using Inforce.Domain.Entities;
using Inforce.Service.Dto.UserDtos;

namespace Inforce.Service.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();

            CreateMap<IEnumerable<User>, IEnumerable<UserDto>>();
        }

    }
}
