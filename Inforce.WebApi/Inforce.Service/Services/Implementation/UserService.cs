using AutoMapper;
using Inforce.Domain.Entities;
using Inforce.Repository.Repo;
using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UserDtos;
using Inforce.Service.Helpers;
using Inforce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Inforce.Service.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IRepository<User> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IConfiguration configiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configiguration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddAsync(CreateUserDto entity)
        {
            if(await _repository.SingleOrDefaultAsync(x => x.Email == entity.Email) != null)
            {
                return false;
            }
            var user = _mapper.Map<User>(entity);
            await _repository.AddAsync(user);
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<User>, List < UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _repository.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<bool> RemoveAsync(int id)
        {
            return await _repository.RemoveAsync(id);
        }

        public async Task<bool> UpdateAsync(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            return await _repository.UpdateAsync(user);
        }

        public async Task<UserDto?> GetMeAsync()
        {
            var result = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);
            var user = await _repository.SingleOrDefaultAsync(user => user.Id == int.Parse(result));
            var response = _mapper.Map<UserDto>(user);
            return response;
        }

        public async Task<AuthResponseDto?> LoginAsync(AuthRequestDto authRequestDto)
        {
            var user = await _repository.SingleOrDefaultAsync(user => user.Email == authRequestDto.Email);
            if(user == null)
            {
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new AuthResponseDto(user,token);
        }
    }
}
