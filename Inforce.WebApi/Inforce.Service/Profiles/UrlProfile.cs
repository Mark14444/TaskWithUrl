using AutoMapper;
using Inforce.Domain.Entities;
using Inforce.Service.Dto.UrlDtos;

namespace Inforce.Service.Profiles
{
    public class UrlProfile:Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, UrlDto>();
            CreateMap<Url, UrlDto>().ReverseMap();
        }
    }
}
