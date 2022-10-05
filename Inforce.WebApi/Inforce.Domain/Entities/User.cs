
namespace Inforce.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; }
        public ICollection<Url>? Urls { get; set; }
    }
}
