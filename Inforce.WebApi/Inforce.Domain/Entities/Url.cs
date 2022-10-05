
namespace Inforce.Domain.Entities
{
    public class Url:BaseEntity
    {
        public string OriginalUrl { get; set; } = null!;
        public string ShortUrl { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
