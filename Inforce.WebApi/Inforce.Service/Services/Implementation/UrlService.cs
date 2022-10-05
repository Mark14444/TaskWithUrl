using AutoMapper;
using Inforce.Domain.Entities;
using Inforce.Repository.Repo;
using Inforce.Service.Dto.UrlDtos;
using Inforce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;

namespace Inforce.Service.Services.Implementation
{
    public class UrlService : IUrlService
    {
        private readonly IRepository<Url> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccesor;
        public UrlService(IRepository<Url> repository,IMapper mapper,IHttpContextAccessor httpContext, IRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccesor = httpContext;
            _userRepository = userRepository;
        }
        private async Task<User> GetUserAsync()
        {
            var result = _httpContextAccesor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);
            var user = await _userRepository.SingleOrDefaultAsync(user => user.Id == int.Parse(result));
            return user!;
        }
        public async Task AddAsync(CreateShortUrlDto entity)
        {
            var user = await GetUserAsync();
            var builder = new StringBuilder(5);

            char offset = 'a';
            int lettersOffset = 26;

            for (var i = 0; i < 5; i++)
            {
                var @char = (char)new Random().Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            string shortUrl = builder.ToString().ToLower();
            var url = new Url() { OriginalUrl = entity.OriginalUrl, ShortUrl = shortUrl, UserId = user.Id };
            await _repository.AddAsync(url);
        }

        public async Task<UrlDto> GetUrlByShortName(string shortUrl)
        {
            var url = await _repository.SingleOrDefaultAsync(x => x.ShortUrl == shortUrl);
            var urlDto = _mapper.Map<UrlDto>(url);
            return urlDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.RemoveAsync(id);
        }

        public async Task<IEnumerable<UrlDto>> GetAllUrlsAsync()
        {
            var urls = await _repository.GetAllAsync();
            var urlsDto = _mapper.Map<IEnumerable<Url>, List<UrlDto>>(urls);
            return urlsDto;
        }

        public async Task<UrlDto> GetUrlByIdAsync(int id)
        {
            var url = await _repository.GetAsync(id);
            return _mapper.Map<UrlDto>(url);
        }
    }
}
