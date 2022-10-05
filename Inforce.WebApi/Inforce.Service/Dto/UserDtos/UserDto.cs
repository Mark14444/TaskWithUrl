using Inforce.Domain;
using Inforce.Service.Dto.UrlDtos;

namespace Inforce.Service.Dto.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; }
        public ICollection<UrlDto>? Urls { get; set; }
    }
}
