using Inforce.Domain.Entities;
using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UserDtos;

namespace Inforce.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(CreateUserDto entity);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto?> GetMeAsync();
        Task<AuthResponseDto?> LoginAsync(AuthRequestDto authRequestDto);
        Task<bool> UpdateAsync(UserDto entity);
        Task<bool> RemoveAsync(int id);

    }
}
