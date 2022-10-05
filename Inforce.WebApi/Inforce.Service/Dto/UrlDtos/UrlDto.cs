
namespace Inforce.Service.Dto.UrlDtos
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortUrl { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
    }
}
