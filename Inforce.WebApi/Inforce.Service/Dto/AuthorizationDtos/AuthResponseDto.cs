using Inforce.Domain.Entities;

namespace Inforce.Service.Dto.AuthorizationDtos
{
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } = null!;
        public AuthResponseDto(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;
        }
    }
}
